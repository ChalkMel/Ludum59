using UnityEngine;
using UnityEngine.UI;

public abstract class BaseWindow : MonoBehaviour
{
  [SerializeField] protected Button closeBtn;
  protected OverloadSystem overload;
    
  public void Init(OverloadSystem overloadSystem)
  {
    overload = overloadSystem;
    if (closeBtn != null)
      closeBtn.onClick.AddListener(Close);
  }

  public virtual void Close()
  {
    overload?.Remove(5);
    Destroy(gameObject);
  }
}