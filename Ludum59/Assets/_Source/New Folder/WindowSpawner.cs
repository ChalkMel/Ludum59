using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
  [SerializeField] private List<GameObject> windowPrefabs;
  [SerializeField] private GameObject adPrefab;
  [SerializeField] private GameObject callPrefab;
  [SerializeField] private float adPercent;
  [SerializeField] private float callPercent;
  [SerializeField] private float adInterval;
  [SerializeField] private float callInterval;
  [SerializeField] private Transform parent;
  [SerializeField] private Transform messageParent;
  [SerializeField] private float spawnInterval = 3f;
  [SerializeField] private int maxWindows = 20;
    
  private OverloadSystem overload;
  private bool _isSized;
  private int spawnedCount = 0;
  private float lastAdTime = -999f;
  private float lastCallTime = -999f;
    
  public void StartSpawning(OverloadSystem overloadSystem)
  {
    overload = overloadSystem;
    StartCoroutine(SpawnRoutine());
  }
    
  private IEnumerator SpawnRoutine()
  {
    while (spawnedCount < maxWindows)
    {
      yield return new WaitForSeconds(spawnInterval);
      
      GameObject prefabToSpawn = null;
      
      bool canShowAd = (adPrefab != null && Time.time - lastAdTime >= adInterval);
      
      bool canShowCall = (callPrefab != null && Time.time - lastCallTime >= callInterval);
      
      if (canShowAd && Random.Range(0, 100) < adPercent)
      {
        prefabToSpawn = adPrefab;
        lastAdTime = Time.time;
      }
      else if (canShowCall && Random.Range(0, 100) < callPercent)
      {
        prefabToSpawn = callPrefab;
        lastCallTime = Time.time;
      }
      else
      {
        prefabToSpawn = windowPrefabs[Random.Range(0, windowPrefabs.Count)];
      }
      
      SpawnWindow(prefabToSpawn);
    }
  }

  private void SpawnWindow(GameObject prefab)
  {
    var window = Instantiate(prefab, parent);
      var rect = window.GetComponent<RectTransform>();
      if (rect != null)
      {
        //rect.sizeDelta = new Vector2(600, 450);

        var parentRect = parent.GetComponent<RectTransform>().rect;
        rect.anchoredPosition = new Vector2(
          Random.Range(-parentRect.width / 2 + 200, parentRect.width / 2 - 200),
          Random.Range(-parentRect.height / 2 + 150, parentRect.height / 2 - 150)
        );
      }

   var baseWindow = window.GetComponent<BaseWindow>();
    baseWindow?.Init(overload);

    spawnedCount++;
    overload?.Add(5);
  }
}