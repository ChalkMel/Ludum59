using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateNotification : MonoBehaviour
{
  [SerializeField] private Button laterButton;
  [SerializeField] private Button updateButton;
    
  private SystemNotificationManager manager;
  private OverloadSystem overload;
    
  public void Setup(SystemNotificationManager mgr, OverloadSystem ovld)
  {
    manager = mgr;
    overload = ovld;
        
    laterButton.onClick.AddListener(() => {
      manager.HandleUpdate(false);
      Destroy(gameObject);
    });
        
    updateButton.onClick.AddListener(() => {
      manager.HandleUpdate(true);
      Destroy(gameObject);
    });
        
    Destroy(gameObject, 10f);
  }
}