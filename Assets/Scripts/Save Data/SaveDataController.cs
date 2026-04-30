using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.VersionControl;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    private static SaveDataController _instance;
    public static SaveDataController Instance => _instance;

    [SerializeField] private SaveDataAsset _saveDataAsset;
    [SerializeField] private AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face> defaultDie;
    [HideInInspector] public SaveData current;

    [SerializeField] private string _directory;
    [SerializeField] private string _fileName;

    private void Awake()
    {
        _instance = this;
        Load();
        //Debug.Log("beep");
        //Debug.Log($"deese count: {current.run.Deese.Count}");
        for (int i = current.run.Deese.Count; i < 6; i++)
        {
            Debug.Log("gurt: yo");
            current.run.Deese.Add(defaultDie);
            Debug.Log("meep");
        }

        //Application.wantsToQuit += () => //Do this for a warning when quiting that you would have unsave data
        //{



        //    return false;
        //};
    }
    //
    private void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        Serializer.Save(current, Path.Combine(Application.persistentDataPath, _directory), _fileName);
        Debug.Log("turtles");
    }

    public void Set(Dictionary<string, object> changes)
    {
        foreach (var kvp in changes)
        {
            current[kvp.Key] = kvp.Value;
        }
    }

    public void Load()
    {
        current = Serializer.Load(_saveDataAsset.SaveData, Path.Combine(Application.persistentDataPath, _directory), _fileName);
    }

    [ContextMenu("Print Dice")]
    public void PrintDice()
    {
        int i = 0;
        int j = 0;

        foreach (var die in SaveDataController.Instance.current.run.Deese)
        {
            i++;
            Debug.Log($"Dice: {i}");
            foreach (var key in die.Keys)
            {
                j++;
                Debug.Log($"Face {j}: {die[key]}");               
            }
            Debug.Log("\n");

        }
    }
}