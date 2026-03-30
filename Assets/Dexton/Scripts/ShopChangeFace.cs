using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopChangeFace : MonoBehaviour
{
    private GameObject _shopDie;
    public GlobalDie dieTexture;
    public DiceScoreCalc diceScoreCalc;

    public void Awake()
    {
        _shopDie = GameObject.Find("ShopDie");
        dieTexture = Resources.Load<GlobalDie>("ScriptableObjects/Dice/Do_Not_Use_This_Or_I_Will_Steal_All_Your_Bagels"); // Change to the die taking place of shop die later
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
            [-_shopDie.transform.up] = (dieTexture.Faces[Vector3.down], Vector3.down),
            [_shopDie.transform.up] = (dieTexture.Faces[Vector3.up], Vector3.up),
            [-_shopDie.transform.right] = (dieTexture.Faces[Vector3.left], Vector3.left),
            [_shopDie.transform.right] = (dieTexture.Faces[Vector3.right], Vector3.right),
            [-_shopDie.transform.forward] = (dieTexture.Faces[Vector3.back], Vector3.back),
            [_shopDie.transform.forward] = (dieTexture.Faces[Vector3.forward], Vector3.forward)
        };

        var ordered = sides.Select(item => item.Key).OrderBy(item => Vector3.Dot(item, Camera.main.transform.forward));

        Vector3 index = sides[ordered.FirstOrDefault()].Item2;

        dieTexture.Faces[index] = newFace;

        diceScoreCalc.points -= newFace.price;

        _shopDie.GetComponent<FaceChange>().UpdateDiceFaces();
    }
}
