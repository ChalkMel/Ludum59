using UnityEngine;
using TMPro;

public class CodeNotification : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI codeText;
    
  private string _code;
  private OverloadSystem _overload;
    
  public void Setup(string verificationCode, OverloadSystem overloadSystem)
  {
    _code = verificationCode;
    _overload = overloadSystem;
    codeText.text = verificationCode;
  }
}