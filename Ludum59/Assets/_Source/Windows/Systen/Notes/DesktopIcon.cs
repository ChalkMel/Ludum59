using UnityEngine;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour
{
  [SerializeField] private Button iconButton;
  [SerializeField] private string iconName = "Заметки";
    
  [SerializeField] private NotesManager notesManager;
    
  private void Start()
  {
    iconButton.onClick.AddListener(OpenNotes);
  }
    
  public void Initialize(NotesManager manager)
  {
    notesManager = manager;
  }
    
  private void OpenNotes()
  {
    notesManager?.OpenNotes();
  }
}