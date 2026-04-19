using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
  [SerializeField] private List<GameObject> windowPrefabs;
  [SerializeField] private Transform parent;
  [SerializeField] private Transform messageParent;
  [SerializeField] private float spawnInterval = 3f;
  [SerializeField] private int maxWindows = 20;
    
  private OverloadSystem overload;
  private int spawnedCount = 0;
    
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
      SpawnRandomWindow();
    }
  }
    
  private void SpawnRandomWindow()
  {
    var prefab = windowPrefabs[Random.Range(0, windowPrefabs.Count)];
    if (prefab.TryGetComponent(out DMNotification messageWindow))
    {
      Instantiate(messageWindow, messageParent);
    }
    else
    {
      var window = Instantiate(prefab, parent);
      
      var rect = window.GetComponent<RectTransform>();
      if (rect != null)
      {
        var parentRect = parent.GetComponent<RectTransform>().rect;
        rect.anchoredPosition = new Vector2(
          Random.Range(-parentRect.width / 2, parentRect.width / 2),
          Random.Range(-parentRect.height / 2, parentRect.height / 2)
        );
      }

      var baseWindow = window.GetComponent<BaseWindow>();
      baseWindow?.Init(overload);
    }

    spawnedCount++;
    overload?.Add(5);
  }
}