using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Graphics")]
    public TMP_Dropdown resolutionDropdown;
    [HideInInspector] public bool distractingVisuals = true;
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
    [SerializeField] private AudioSource sfx;
    [SerializeField] private Slider sfxSlider;


    UnityEngine.Resolution[] resolutions; // Change type to UnityEngine.Resolution[]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Debug.Log("FullScreen Changed");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetDistractingVisuals(bool value)
    {
        distractingVisuals = value;
    }
}
