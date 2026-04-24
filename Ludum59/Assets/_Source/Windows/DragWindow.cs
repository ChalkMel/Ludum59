using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler
{
  [Header("Настройки")]
  [SerializeField] private bool clampToParent = true;
    
  private RectTransform _rectTransform;
  private RectTransform _parentRect;
  private Canvas canvas; 
    
  private void Awake()
  {
    _rectTransform = GetComponent<RectTransform>();
    canvas = GetComponentInParent<Canvas>();
        
    if (clampToParent && transform.parent != null)
      _parentRect = transform.parent.GetComponent<RectTransform>();
  }
    
  public void OnBeginDrag(PointerEventData eventData)
  {
    transform.SetAsLastSibling();
  }
    
  public void OnDrag(PointerEventData eventData)
  {
    Vector2 delta = eventData.delta / canvas.scaleFactor;
    Vector2 newPosition = _rectTransform.anchoredPosition + delta;
        
    if (clampToParent && _parentRect != null)
    {
      float left = -_parentRect.rect.width / 2 + _rectTransform.rect.width / 2;
      float right = _parentRect.rect.width / 2 - _rectTransform.rect.width / 2;
      float top = _parentRect.rect.height / 2 - _rectTransform.rect.height / 2;
      float bottom = -_parentRect.rect.height / 2 + _rectTransform.rect.height / 2;
            
      newPosition.x = Mathf.Clamp(newPosition.x, left, right);
      newPosition.y = Mathf.Clamp(newPosition.y, bottom, top);
    }
        
    _rectTransform.anchoredPosition = newPosition;
  }
}