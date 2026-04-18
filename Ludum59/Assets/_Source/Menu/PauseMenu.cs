using System;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button quitButton;
    private bool _isPaused;
    
    private void Start()
    {
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(Settings);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                Play();
            else
                Pause();
        }
    }

    private void Play()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
        _isPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
        _isPaused = true;
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
