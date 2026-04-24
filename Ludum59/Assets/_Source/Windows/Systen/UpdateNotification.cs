using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateNotification : MonoBehaviour
{
  [SerializeField] private Button laterButton;
  [SerializeField] private Button updateButton;
    
  private SystemNotificationManager _manager;
  private OverloadSystem _overload;
    
  public void Setup(SystemNotificationManager mgr, OverloadSystem ovld)
  {
    _manager = mgr;
    _overload = ovld;
        
    laterButton.onClick.AddListener(() => {
      _manager.HandleUpdate(false);
      Destroy(gameObject);
    });
        
    updateButton.onClick.AddListener(() => {
      _manager.HandleUpdate(true);
      Destroy(gameObject);
    });
        
    Destroy(gameObject, 10f);
  }
}