using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
  [SerializeField] private Button playButton;
 // [SerializeField] private Button settingsButton;
  [SerializeField] private Button quitButton;
  [SerializeField] private GameObject settingsPanel;
  [SerializeField] private bool isMainMenu;

  private void Start()
  {
    playButton.onClick.AddListener(Play);
    //settingsButton.onClick.AddListener(Settings);
    //quitButton.onClick.AddListener(Quit);
  }
  private void Play()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  private void Settings()
  {
    settingsPanel.SetActive(true);
  }

  private void Quit()
  {
    settingsPanel.SetActive(false);
  }
}
