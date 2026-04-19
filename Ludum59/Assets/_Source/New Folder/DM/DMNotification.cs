using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DMNotification : MonoBehaviour
{
  [SerializeField] private Image avatar;
  [SerializeField] private TextMeshProUGUI previewText;
  [SerializeField] private Button closeBtn;
  [SerializeField] private Button clickArea;
    
  private DMNotificationManager.DMMessage message;
  private DMNotificationManager manager;
  private OverloadSystem overload;
    
  public void Setup(DMNotificationManager.DMMessage msg, DMNotificationManager mgr, OverloadSystem ovld)
  {
    message = msg;
    manager = mgr;
    overload = ovld;
        
    avatar.sprite = msg.avatar;
    previewText.text = msg.preview;
        
    closeBtn.onClick.AddListener(() => Destroy(gameObject));
    clickArea.onClick.AddListener(OpenWindow);
  }
    
  private void OpenWindow()
  {
    manager.OpenWindow(message);
    Destroy(gameObject);
  }
}