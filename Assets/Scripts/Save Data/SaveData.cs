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
}


[Serializable]
public class Run // data saved in that run :skull:
{
    public int Points;
    public List<AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face>> Deese;
}