using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  [Header("Refs")]
  [SerializeField] private Slider overloadSlider;
  [SerializeField] private TextMeshProUGUI overloadText;
  [SerializeField] private DMNotificationManager dmManager;
  [SerializeField] private SystemNotificationManager systemManager;
  [SerializeField] private OverloadSystem overload;
  
  [SerializeField] private NotesManager notesManager;
  [SerializeField] private DesktopIcon desktopIcon;
  [SerializeField] private Image gameOverPanel;

  [SerializeField] private Sprite overloadImage;
  [SerializeField] private Sprite EndImage;
  [SerializeField] private Sprite virusImage;
    
  [Header("Spawner")]
  [SerializeField] private WindowSpawner spawner;

  [SerializeField] private GameObject spawnArea;
  
    
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
    Change(overloadImage);
  }

  public void EndGame()
  {
    Change(EndImage);
  }

  public void VirusEnd()
  {
    Change(virusImage);
  }

  private void Change(Sprite screen)
  {
    gameOverPanel.gameObject.SetActive(true);
    var canvas = gameOverPanel.GetComponent<CanvasGroup>();
    gameOverPanel.sprite = screen;
    canvas.DOFade(1f, .5f).onComplete += () =>
    {
      gameOverPanel.transform.SetAsLastSibling();
      Time.timeScale = 0;
      Destroy(spawnArea);
    };
  }
}