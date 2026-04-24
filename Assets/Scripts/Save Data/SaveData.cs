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

    public object this[string index]
    {
        readonly get => index switch
        {
            "run" => run,
            "Settings" => Settings,
            _ => null
        };
        set
        {
            switch (index)
            {
                case "run":
                    run = value as Run;
                    break;

                case "Settings":
                    Settings = value as UserSettings;
                    break;
            }
        }
    }
}


[Serializable]
public class Run // data saved in that run :skull:
{
    public int Points;
    public int TotalEarnedPoints;
    public int TotalDebtPayment;
    public int CurrentDebt = 8000;
    public float CurrentInterestRate;
    public float CurrentInterestIncrease;
    public List<AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face>> Deese;
    //public float InterestRate
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
    }; 
    //SaveDataController.Instance.current.Settings.IsFullscreen = Resolution;

    public bool IgnoreSelectWarning = false; //This doesnt need to go into settings menu
    public bool ShowTutorial = true; //This should go into settings menu

    public List<int> ClosedTutorials = new ();

}