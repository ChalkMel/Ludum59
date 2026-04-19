using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotesWindow : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI passwordText;
  [SerializeField] private Button closeButton;
  [SerializeField] private string passwordHint = "Пароль: "; // текст перед паролем
  
  private OverloadSystem overload;
  private string password;
  private NotesManager manager;
    
  public void Setup(string pwd, NotesManager mgr, OverloadSystem overloadSystem)
  {
    password = pwd;
    manager = mgr;
    overload = overloadSystem;
    passwordText.text = passwordHint + password;

    closeButton.onClick.AddListener(Close);
  }

  private void Close()
  {
    Destroy(gameObject);
    overload.Remove(1);
  }
}