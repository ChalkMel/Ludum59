using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  [Header("UI")]
  [SerializeField] private Slider overloadSlider;
  [SerializeField] private TextMeshProUGUI overloadText;
  [SerializeField] private DMNotificationManager dmManager;
  [SerializeField] private SystemNotificationManager systemManager;
  
  [SerializeField] private NotesManager notesManager;
  [SerializeField] private DesktopIcon desktopIcon;
    
  [Header("Spawner")]
  [SerializeField] private WindowSpawner spawner;
    
  private OverloadSystem overload = new OverloadSystem();
    
  private void Start()
  {
    overload.OnChanged += UpdateUI;
    overload.OnGameOver += GameOver;
    
    dmManager.StartSpawning(overload);
    spawner.StartSpawning(overload);
    notesManager.Initialize(overload);
    systemManager.Initialize(overload, notesManager);
  }
    
  private void UpdateUI(int value)
  {
    overloadSlider.value = value;
    overloadText.text = $"Overload: {value}%";
  }
    
  private void GameOver()
  {
    Debug.Log("GAME OVER!");
    Time.timeScale = 0;
    //TODO: экран
  }
}