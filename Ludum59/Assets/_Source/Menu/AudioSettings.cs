using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
  [SerializeField] private Button settingsButton;
  [SerializeField] private Image buttonImage;
  [SerializeField] private Sprite loudSprite;
  [SerializeField] private Sprite quietSprite;
  [SerializeField] private Sprite muteSprite;
  [SerializeField] private AudioMixerGroup mixer;
  
  private const string master_volume = "MasterVolume";
  private int _currentLevel = 0;
  private float[] _volumes = { 1f, -20f, -80f };
    
  private void Start()
  {
    settingsButton = GetComponent<Button>(); 
    buttonImage = GetComponent<Image>();
    settingsButton.onClick.AddListener(CycleVolume);
    ApplyVolume(0);
  }
    
  private void CycleVolume()
  {
    _currentLevel = (_currentLevel + 1) % 3;
    ApplyVolume(_currentLevel);
  }

  private void ApplyVolume(int level)
  {
    mixer.audioMixer.SetFloat(master_volume, _volumes[level]);
    Debug.Log(_volumes[level]);
    switch (level)
    {
      case 0:
        buttonImage.sprite = loudSprite;
        break;
      case 1:
        buttonImage.sprite = quietSprite;
        break;
      case 2:
        buttonImage.sprite = muteSprite;
        break;
    }
  }
}