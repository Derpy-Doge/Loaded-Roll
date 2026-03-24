using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopChangeFace : MonoBehaviour
{
    public GameObject shopDie;
    public GlobalDie dieTexture;
    public Face newFace;

    public void Start()
    {
        shopDie = GameObject.Find("ShopDie");
        dieTexture = Resources.Load<GlobalDie>("ScriptableObjects/Dice/Do_Not_Use_This_Or_I_Will_Steal_All_Your_Bagels");
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

    public void ChangeFace()
    {
        Dictionary<Vector3, Face> sides = new()
        {
            [-shopDie.transform.up] = dieTexture.Faces[Vector3.down],
            [shopDie.transform.up] = dieTexture.Faces[Vector3.up],
            [-shopDie.transform.right] = dieTexture.Faces[Vector3.left],
            [shopDie.transform.right] = dieTexture.Faces[Vector3.right],
            [-shopDie.transform.forward] = dieTexture.Faces[Vector3.back],
            [shopDie.transform.forward] = dieTexture.Faces[Vector3.forward]
        };

        var ordered = sides.Select(item => item.Key).OrderBy(item => Vector3.Dot(item, Camera.main.transform.forward));

        dieTexture.Faces[ordered.FirstOrDefault()] = newFace;

        shopDie.GetComponent<FaceChange>().UpdateDiceFaces();
    }
}
