using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeNotification : MonoBehaviour
{
  [Header("UI")]
  [SerializeField] private TextMeshProUGUI codeText;
  [SerializeField] private TextMeshProUGUI titleText;
  [SerializeField] private Button closeButton;
    
  private string code;
  private OverloadSystem overload;
    
  public void Setup(string verificationCode, OverloadSystem overloadSystem)
  {
    code = verificationCode;
    overload = overloadSystem;
        
    if (codeText != null)
      codeText.text = verificationCode;
        
    if (titleText != null)
      titleText.text = "Код подтверждения";
        
    if (closeButton != null)
      closeButton.onClick.AddListener(() => Destroy(gameObject));
    
  }
}