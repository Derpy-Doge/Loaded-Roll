using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Graphics")]
    [Header("FullScreen")]
    [SerializeField] private Toggle fullscreenToggle;
    [Header("Quality")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [Header("Resolution")]
    public TMP_Dropdown resolutionDropdown;
    [Header("HDR")]
    [HideInInspector] public bool distractingVisuals = true;
    [SerializeField] private Toggle distractingVisualsToggle;
    [Space]
    [Header("Audio")]
    [Header("MainMenu")]
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private Slider mainMenuMusicSlider;
    [Space]
    [Header("Game")]
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private Slider gameMusicSlider;
    [Space]
    [Header("SFX")]
    public AudioSource sfx;
    public AudioClip Eraser;
    public AudioClip Marker;
    [SerializeField] private Slider sfxSlider;


    UnityEngine.Resolution[] resolutions; // Change type to UnityEngine.Resolution[]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Fullscreen Calculation For Save Data
        Screen.fullScreen = SaveDataController.Instance.current.Settings.IsFullscreen;
        fullscreenToggle.isOn = SaveDataController.Instance.current.Settings.IsFullscreen;

        //HDR Calculation For Save Data
        distractingVisuals = SaveDataController.Instance.current.Settings.IsDistracting;
        distractingVisualsToggle.isOn = SaveDataController.Instance.current.Settings.IsDistracting;

        //Quality Calculation For Save Data
        QualitySettings.SetQualityLevel((int)SaveDataController.Instance.current.Settings.Quality);
        qualityDropdown.value = (int)SaveDataController.Instance.current.Settings.Quality;
        qualityDropdown.RefreshShownValue();

        //Resolution Calculation
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Audio Calculation For Save Data
        mainMenuMusic = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        gameMusic = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();


        mainMenuMusicSlider.value = SaveDataController.Instance.current.Settings.MenuMusicVolume;
        gameMusicSlider.value = SaveDataController.Instance.current.Settings.MusicVolume;
        sfxSlider.value = SaveDataController.Instance.current.Settings.SFXVolume;

        mainMenuMusicSlider.onValueChanged.AddListener(delegate { SetMMM(mainMenuMusicSlider.value); });
        gameMusicSlider.onValueChanged.AddListener(delegate { SetGM(gameMusicSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSFX(sfxSlider.value); });
    }

    public void SetMMM(float value)
    { 
        Debug.Log("Menu Music Volume Changed");
        SaveDataController.Instance.current.Settings.MenuMusicVolume = value;
        mainMenuMusic.volume = SaveDataController.Instance.current.Settings.MenuMusicVolume;
    }

    public void SetGM(float value)
    {
        SaveDataController.Instance.current.Settings.MusicVolume = value;
        gameMusic.volume = SaveDataController.Instance.current.Settings.MusicVolume;
    }

    public void SetSFX(float value)
    {
        SaveDataController.Instance.current.Settings.SFXVolume = value;
        sfx.volume = SaveDataController.Instance.current.Settings.SFXVolume;
    }
    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
public void Changed(bool value)
    {
        Screen.fullScreen = value;
        SaveDataController.Instance.current.Settings.IsFullscreen = value;
        Debug.Log("FullScreen Changed");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SaveDataController.Instance.current.Settings.Quality = (UserSettings.QualityTypes)qualityIndex;
    }

    public void SetDistractingVisuals(bool value)
    {
        distractingVisuals = value;
        SaveDataController.Instance.current.Settings.IsDistracting = value;
    }

    public void Erase()
    {
        Debug.Log("Erase");
        sfx.clip = Eraser;
        sfx.Play();
    }

    public void Unerase()
    {
        Debug.Log("unerase");
        sfx.clip = Marker;
        sfx.Play();
    }
}
