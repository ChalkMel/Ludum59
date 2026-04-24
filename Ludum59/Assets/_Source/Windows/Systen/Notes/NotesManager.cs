using System;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
  [SerializeField] private string password;
  [SerializeField] private GameObject notesWindowPrefab;
  [SerializeField] private Transform windowsParent;
    
  private OverloadSystem _overload;
  private string _currentPassword;


  public void Initialize(OverloadSystem overloadSystem)
  {
    _overload = overloadSystem;
    _currentPassword = password;
  }
    
  public void OpenNotes()
  {
    _overload?.Add(1);
        
    var window = Instantiate(notesWindowPrefab, windowsParent);
    var notesWin = window.GetComponent<NotesWindow>();
    notesWin.Setup(_currentPassword, this, _overload);
  }
    
  public bool CheckPassword(string input)
  {
    return input == _currentPassword;
  }
}