using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Dice", menuName = "ScriptableObjects/Dice")]
public class GlobalDie : ScriptableObject
{
    //Place in save data and make a list
    public AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face> Faces;

}
