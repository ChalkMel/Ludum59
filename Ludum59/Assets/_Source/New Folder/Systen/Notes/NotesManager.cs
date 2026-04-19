using System;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
  [SerializeField] private string password = "admin123"; // пароль из заметок
  [SerializeField] private GameObject notesWindowPrefab;
  [SerializeField] private Transform windowsParent;
    
  private OverloadSystem overload;
  private string currentPassword;


  public void Initialize(OverloadSystem overloadSystem)
  {
    overload = overloadSystem;
    currentPassword = password;
  }
    
  public void OpenNotes()
  {
    overload?.Add(1); // +1 перегрузки при открытии
        
    var window = Instantiate(notesWindowPrefab, windowsParent);
    var notesWin = window.GetComponent<NotesWindow>();
    notesWin.Setup(currentPassword, this, overload);
  }
    
  public bool CheckPassword(string input)
  {
    return input == currentPassword;
  }
}