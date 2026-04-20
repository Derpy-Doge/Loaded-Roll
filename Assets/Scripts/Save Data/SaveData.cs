using System;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public const string fileName = "/saveData.json";  
    // i need stuff to save :skull:
}

[Serializable]
public struct SaveData
{
    
   public Run run; //SaveDataController.Instance.current.Run
    public UserSettings Settings;
}


[Serializable]
public class Run // data saved in that run :skull:
{
    public int Points;
    public List<AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face>> Deese;
}

[Serializable]
public class UserSettings
{
    public enum QualityTypes
    {
        VeryLow = 0,
        Low = 1,
        Medium = 2,
        High = 3,
        VeryHigh = 4,
        Ultra = 5

    }


    public float MusicVolume = 1f; //SaveDataController.Instance.current.Settings.MusicVolume = x.xf;
    public float MenuMusicVolume = 1f; //SaveDataController.Instance.current.Settings.MenuMusicVolume = x.xf;
    public float SFXVolume = 1f; //SaveDataController.Instance.current.Settings.SFXVolume = x.xf;
    public bool IsFullscreen = true; //SaveDataController.Instance.current.Settings.IsFullscreen = t/f;
    public QualityTypes Quality = QualityTypes.High; //SaveDataController.Instance.current.Settings.IsFullscreen = QualityTypes.x;
    public bool IsDistracting = true; //SaveDataController.Instance.current.Settings.IsFullscreen = t/f;
    public Resolution Res = new()
    {
        width = Screen.width,
        height = Screen.height,
    }; //SaveDataController.Instance.current.Settings.IsFullscreen = Resolution;

}