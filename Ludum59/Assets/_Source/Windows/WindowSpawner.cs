using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
  [SerializeField] private List<GameObject> windowPrefabs;
  [SerializeField] private List<GameObject> adPrefab;
  [SerializeField] private GameObject callPrefab;
  [SerializeField] private float adPercent;
  [SerializeField] private float callPercent;
  [SerializeField] private float adInterval;
  [SerializeField] private float callInterval;
  [SerializeField] private Transform parent;
  [SerializeField] private Transform messageParent;
  [SerializeField] public float spawnInterval = 3f;
    
  private OverloadSystem _overload;
  private bool _isSized;
  private int spawnedCount = 0;
  private float _lastAdTime = -999f;
  private float _lastCallTime = -999f;
    
  public void StartSpawning(OverloadSystem overloadSystem)
  {
    _overload = overloadSystem;
    StartCoroutine(SpawnRoutine());
  }
    
  private IEnumerator SpawnRoutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(spawnInterval);
      
      GameObject prefabToSpawn = null;
      
      bool canShowAd = (adPrefab != null && Time.time - _lastAdTime >= adInterval);
      
      bool canShowCall = (callPrefab != null && Time.time - _lastCallTime >= callInterval);
      
      if (canShowAd && Random.Range(0, 100) < adPercent)
      {
        prefabToSpawn = adPrefab[Random.Range(0, adPrefab.Count)];
        _lastAdTime = Time.time;
      }
      else if (canShowCall && Random.Range(0, 100) < callPercent)
      {
        prefabToSpawn = callPrefab;
        _lastCallTime = Time.time;
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
        var parentRect = parent.GetComponent<RectTransform>().rect;
        rect.anchoredPosition = new Vector2(
          Random.Range(-parentRect.width / 2 + 200, parentRect.width / 2 - 200),
          Random.Range(-parentRect.height / 2 + 150, parentRect.height / 2 - 150)
        );
      }

   var baseWindow = window.GetComponent<BaseWindow>();
    baseWindow?.Init(_overload);

    spawnedCount++;
    _overload?.Add(5);
  }
}