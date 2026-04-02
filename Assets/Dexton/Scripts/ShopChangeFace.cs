using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopChangeFace : MonoBehaviour
{
    private GameObject _shopDie;
    public GlobalDie[] dieTexture;
    public DiceScoreCalc diceScoreCalc;
    //public FaceChange faceChange;

    public void Awake()
    {
        _shopDie = GameObject.Find("ShopDie");
        dieTexture = FindObjectsByType<GlobalDie>(FindObjectsSortMode.None); // Change to the die taking place of shop die later
        diceScoreCalc = FindAnyObjectByType<DiceScoreCalc>();
    }

    public void ChangeFace(Face newFace)
    {
        if (diceScoreCalc.points < newFace.price)
        {
            Debug.Log("Not enough points to change face.");
            return;
        }

        Dictionary<Vector3, (Face, Vector3)> sides = new()
        {
            [-_shopDie.transform.up] = (FindAnyObjectByType<FaceChange>().Dice.Faces[Vector3.down], Vector3.down),
            [_shopDie.transform.up] = (FindAnyObjectByType<FaceChange>().Dice.Faces[Vector3.up], Vector3.up),
            [-_shopDie.transform.right] = (FindAnyObjectByType<FaceChange>().Dice.Faces[Vector3.left], Vector3.left),
            [_shopDie.transform.right] = (FindAnyObjectByType<FaceChange>().Dice.Faces[Vector3.right], Vector3.right),
            [-_shopDie.transform.forward] = (FindAnyObjectByType<FaceChange>().Dice.Faces[Vector3.back], Vector3.back),
            [_shopDie.transform.forward] = (FindAnyObjectByType<FaceChange>().Dice.Faces[Vector3.forward], Vector3.forward)
        };

        var ordered = sides.Select(item => item.Key).OrderBy(item => Vector3.Dot(item, Camera.main.transform.forward));

        Vector3 index = sides[ordered.FirstOrDefault()].Item2;

        FindAnyObjectByType<FaceChange>().Dice.Faces[index] = newFace;

        diceScoreCalc.points -= newFace.price;

        FindAnyObjectByType<FaceChange>().UpdateDiceFaces();
    }
}
