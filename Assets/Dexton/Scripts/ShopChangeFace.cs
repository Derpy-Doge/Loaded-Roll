using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopChangeFace : MonoBehaviour
{
    private GameObject _shopDie;
    private GlobalDie _dieTexture;

    public void Start()
    {
        _shopDie = GameObject.Find("ShopDie");
        _dieTexture = Resources.Load<GlobalDie>("ScriptableObjects/Dice/Do_Not_Use_This_Or_I_Will_Steal_All_Your_Bagels");
    }

    public void Awake()
    {
        //foreach (Face face in faces)
        //{
        //    if (face.name == gameObject.name)
        //    {
        //        newFace = face;
        //        break;
        //    }
        //}
    }

    public void ChangeFace(Face newFace)
    {
        Dictionary<Vector3, (Face, Vector3)> sides = new()
        {
            [-_shopDie.transform.up] = (_dieTexture.Faces[Vector3.down], Vector3.down),
            [_shopDie.transform.up] = (_dieTexture.Faces[Vector3.up], Vector3.up),
            [-_shopDie.transform.right] = (_dieTexture.Faces[Vector3.left], Vector3.left),
            [_shopDie.transform.right] = (_dieTexture.Faces[Vector3.right], Vector3.right),
            [-_shopDie.transform.forward] = (_dieTexture.Faces[Vector3.back], Vector3.back),
            [_shopDie.transform.forward] = (_dieTexture.Faces[Vector3.forward], Vector3.forward)
        };

        var ordered = sides.Select(item => item.Key).OrderBy(item => Vector3.Dot(item, Camera.main.transform.forward));

        Vector3 index = sides[ordered.FirstOrDefault()].Item2;
        _dieTexture.Faces[index] = newFace;

        _shopDie.GetComponent<FaceChange>().UpdateDiceFaces();
    }
}
