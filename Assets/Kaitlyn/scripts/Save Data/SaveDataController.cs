using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    private static SaveDataController _instance;
    public static SaveDataController Instance => _instance;

    [SerializeField] private SaveDataAsset _saveDataAsset;
    [HideInInspector] public SaveData current;

    [SerializeField] private string _directory;
    [SerializeField] private string _fileName;

    private void Start()
    {
        _instance = this;
        Load();
        Debug.Log("beep");
        Debug.Log($"deese count: {current.run.Deese.Count}");
        if (current.run.Deese.Count < 1)
        {
            Debug.Log("gurt: yo");
            var test = new AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face>();
            test[Vector3.up] = Resources.Load<Face>("ScriptableObjects/Faces/2 Pips");
            test[Vector3.down] = Resources.Load<Face>("ScriptableObjects/Faces/1 Pip");
            test[Vector3.left] = Resources.Load<Face>("ScriptableObjects/Faces/3 Pips");
            test[Vector3.right] = Resources.Load<Face>("ScriptableObjects/Faces/4 Pips");
            test[Vector3.forward] = Resources.Load<Face>("ScriptableObjects/Faces/1 Pip");
            test[Vector3.back] = Resources.Load<Face>("ScriptableObjects/Faces/3 Pips");

            current.run.Deese.Add(test);
            Debug.Log("meep");
        }
    }
    //
    private void OnDestroy()
    {
        //Save();
    }

    public void Save()
    {
        Serializer.Save(current, Path.Combine(Application.persistentDataPath, _directory), _fileName);
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