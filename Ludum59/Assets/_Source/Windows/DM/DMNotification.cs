using UnityEngine;
using UnityEngine.UI;
public class DMNotification : MonoBehaviour
{
  [SerializeField] private Image notificationImage;
  [SerializeField] private Button closeBtn;
  [SerializeField] private Button clickArea;
    
  private DMNotificationManager.DMCharacter character;
  private DMNotificationManager.DMMessage message;
  private DMNotificationManager manager;
  private OverloadSystem overload;
    
  public void Setup(DMNotificationManager.DMCharacter chr,
    DMNotificationManager.DMMessage msg,
    DMNotificationManager mgr,
    OverloadSystem ovld)
  {
    character = chr;
    message = msg;
    manager = mgr;
    overload = ovld;
        
    notificationImage.sprite = msg.notificationImage;
    
    closeBtn.onClick.AddListener(() => {
      OpenChat();
    });
    clickArea.onClick.AddListener(OpenChat);
  }
    
  private void OpenChat()
  {
    manager.OpenChat(character, message);
    Destroy(gameObject);
  }
}