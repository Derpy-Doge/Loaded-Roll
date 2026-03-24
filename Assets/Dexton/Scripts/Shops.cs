using UnityEngine;

public class Shops : MonoBehaviour
{
    public GameObject shopArea;
    public GameObject buyButton;
    public Face[] faces;


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
            
            instance.GetComponent<ShopChangeFace>().newFace = face;
            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(face.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
