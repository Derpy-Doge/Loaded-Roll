using UnityEngine;
using UnityEngine.UI;

public class Shops : MonoBehaviour
{
    public ShopChangeFace shopChangeFace;
    public Face[] faces;

    public GameObject shopArea, buyButton, shopDie;

    public void Awake()
    {
        shopDie = GameObject.Find("ShopDie");
        shopArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/BuyButton");
        faces = Resources.LoadAll<Face>("ScriptableObjects/Faces");
    }

    public void Start()
    {
        foreach (Face face in faces)
        {
            GameObject instance = Instantiate(buyButton, shopArea.transform);
            
            instance.GetComponent<Image>().sprite = Sprite.Create(face.Texture, new Rect(0, 0, face.Texture.width, face.Texture.height), new Vector2(0.5f, 0.5f));
            instance.GetComponentInChildren<TMPro.TMP_Text>().enabled = false;
            instance.GetComponent<Button>().onClick.AddListener(() => shopChangeFace.ChangeFace(face));
            instance.GetComponent<Button>().onClick.AddListener(() => shopDie.GetComponent<FaceChange>().Dice.gameObject.GetComponent<FaceChange>().UpdateDiceFaces());
        }
    }
}
