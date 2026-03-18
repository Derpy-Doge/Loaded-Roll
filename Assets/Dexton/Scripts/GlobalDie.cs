using System.ComponentModel;
using UnityEngine;

public class GlobalDie : MonoBehaviour
{
    public static GlobalDie Instance { get; private set; }

    //Place in save data
    public AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face> Faces;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
