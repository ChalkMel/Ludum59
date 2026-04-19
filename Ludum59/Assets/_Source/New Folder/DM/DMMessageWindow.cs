using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DMMessageWindow : MonoBehaviour
{
  [SerializeField] private Image avatar;
  [SerializeField] private TextMeshProUGUI senderText;
  [SerializeField] private TextMeshProUGUI messageText;
  [SerializeField] private Button option1;
  [SerializeField] private Button option2;
  [SerializeField] private TextMeshProUGUI option1Text;
  [SerializeField] private TextMeshProUGUI option2Text;
  [SerializeField] private Button closeBtn;
    
  private DMNotificationManager.DMMessage message;
  private OverloadSystem overload;
    
  public void Setup(DMNotificationManager.DMMessage msg, OverloadSystem ovld)
  {
    message = msg;
    overload = ovld;
        
    avatar.sprite = msg.avatar;
    senderText.text = msg.sender;
    messageText.text = msg.fullMessage;
    option1Text.text = msg.option1;
    option2Text.text = msg.option2;
        
    option1.onClick.AddListener(() => { overload?.Remove(5); Destroy(gameObject); });
    option2.onClick.AddListener(() => { overload?.Remove(5); Destroy(gameObject); });
    closeBtn.onClick.AddListener(() => {overload?.Remove(5); Destroy(gameObject); });
  }
}