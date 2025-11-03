using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _fullscreenToggle;

    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string QUALITY_KEY = "QualityLevel";
    private const string FULLSCREEN_KEY = "Fullscreen";

    void Start()
    {
        LoadSettings();
    }

    public void SetMasterVolume(float volume)
    {
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(QUALITY_KEY, qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);
    }

    private void LoadSettings()
    {
        if (_masterVolumeSlider != null)
        {
            float masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
            _masterVolumeSlider.value = masterVolume;
            SetMasterVolume(masterVolume);
        }

        if (_musicVolumeSlider != null)
        {
            float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
            _musicVolumeSlider.value = musicVolume;
            SetMusicVolume(musicVolume);
        }

        if (_sfxVolumeSlider != null)
        {
            float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
            _sfxVolumeSlider.value = sfxVolume;
            SetSFXVolume(sfxVolume);
        }

        if (_qualityDropdown != null)
        {
            int quality = PlayerPrefs.GetInt(QUALITY_KEY, QualitySettings.GetQualityLevel());
            _qualityDropdown.value = quality;
            SetQuality(quality);
        }

        if (_fullscreenToggle != null)
        {
            bool isFullscreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;
            _fullscreenToggle.isOn = isFullscreen;
            SetFullscreen(isFullscreen);
        }
    }
}
