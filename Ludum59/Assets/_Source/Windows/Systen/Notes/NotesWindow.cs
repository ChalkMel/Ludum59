using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotesWindow : MonoBehaviour
{
  [SerializeField] private Button closeButton;
  
  private OverloadSystem _overload;
  private string _password;
  private NotesManager _manager;
    
  public void Setup(string pwd, NotesManager mgr, OverloadSystem overloadSystem)
  {
    _password = pwd;
    _manager = mgr;
    _overload = overloadSystem;

    closeButton.onClick.AddListener(Close);
  }

  private void Close()
  {
    Destroy(gameObject);
    _overload.Remove(1);
  }
}