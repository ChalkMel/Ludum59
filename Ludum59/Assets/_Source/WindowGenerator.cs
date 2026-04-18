using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGenerator : MonoBehaviour
{
  [SerializeField] private List<GameObject> _windowPrefabs;
  [SerializeField] private float timer;
  [SerializeField] private int _openedCount = 0;
  [SerializeField] private int maxCount;
  [SerializeField] private RectTransform _windowParent;
  [SerializeField] private Slider _slider;
    
  private RectTransform _parentRect;
  private Vector2 _parentSize;

  private void Awake()
  {
    _parentRect = _windowParent;
    _parentSize = _parentRect.rect.size;
    _slider.maxValue = maxCount;
    StartCoroutine(GenerateWindowsRoutine());
  }

  IEnumerator GenerateWindowsRoutine()
  {
    while (_openedCount <= maxCount)
    {
      GenerateWindow();
      yield return new WaitForSeconds(timer);
    }
  }

  private void GenerateWindow()
  { 
    var posX = Random.Range(-_parentSize.x / 2, _parentSize.x / 2);
    var posY = Random.Range(-_parentSize.y / 2, _parentSize.y / 2);
        
    var randomPrefab = _windowPrefabs[Random.Range(0, _windowPrefabs.Count)];
    GameObject newWindow = Instantiate(randomPrefab, _windowParent);
    
    RectTransform rectTransform = newWindow.GetComponent<RectTransform>();
    rectTransform.anchoredPosition = new Vector2(posX, posY);
    
    _openedCount++;
    _slider.value += 5;
  }
}