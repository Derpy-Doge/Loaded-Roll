using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopChangeFace : MonoBehaviour
{
    public GameObject shopDie;
    public GlobalDie dieTexture;

    public void ReadFace()
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

        var ordered = sides.Select(item => item.Key).OrderBy(item => Vector3.Dot(item, -Camera.main.transform.forward));
        
        Debug.Log(sides[ordered.FirstOrDefault()].name);

    }
}
