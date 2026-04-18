using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider musicSlider;

    private const string master_volume_name = "SFXVolume";
    private const string music_volume_name = "MusicVolume";
    public static AudioSettings Instance { get; private set; }

    private void Awake()
    {
        #region Singleton

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        #endregion
        
        SFXSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
    }

    #region Load
    private void Start()
    {
        TryLoadSettings(music_volume_name, musicSlider);
        TryLoadSettings(master_volume_name, SFXSlider);
    }
    private void TryLoadSettings(string key, Slider slider)
    {
        if (PlayerPrefs.HasKey(key))
        {
            float volume = PlayerPrefs.GetFloat(key);
            slider.value = volume;
        }
    }
    #endregion

    private void OnSFXVolumeSliderChanged(float value) =>
        SetMixerVolume(value, master_volume_name);
    
    private void OnMusicVolumeSliderChanged(float value) =>
        SetMixerVolume(value, music_volume_name);

    private void SetMixerVolume(float value, string paramName)
    {
        float volume = GetVolumeDb(value);

        audioMixer.SetFloat(paramName, volume);
        PlayerPrefs.SetFloat(paramName, value);
    }
    
    private float GetVolumeDb(float value) => 
        Mathf.Log10(value) * 20;
    
}