using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
    {
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.25f); // saved volume settings
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.00f); // saved volume settings
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;
        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);

        // listener for when slider changes volume value
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    public void SetMusicVolume(float volume)
    {
        // linear value converted to log value for audio mixer
        float volumeDb = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("MusicVolume", volumeDb);

        //  new volume value is saved
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        // linear value converted to log value for audio mixer
        float volumeDb = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("SFXVolume", volumeDb);

        //  new volume value is saved
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
