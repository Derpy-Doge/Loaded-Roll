using UnityEngine;
using UnityEngine.UI;

public class Shops : MonoBehaviour
{
    public ShopChangeFace shopChangeFace;
    public Face[] faces;

    public GameObject shopArea, buyButton;

    public void Awake()
    {
        shopArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/BuyButton");
        faces = Resources.LoadAll<Face>("ScriptableObjects/Faces");
    }

    public void Start()
    {
        foreach (Face face in faces)
        {
            GameObject instance = Instantiate(buyButton, shopArea.transform);
            
            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(face.name);
            instance.GetComponent<Button>().onClick.AddListener(() => shopChangeFace.ChangeFace(face));
        }
    }
}
