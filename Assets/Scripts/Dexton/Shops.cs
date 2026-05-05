using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shops : MonoBehaviour
{
    public ShopChangeFace shopChangeFace;
    public Face[] allFaces;
    private Face[] randomFaces;

    public string format;

    public GameObject shopArea,/* this is a comment*/ buyButton, shopDie, restockButton;
    public int restockCount;
    public Texture2D soldOut;

    public int RestockPrice => (int)restockPriceCurve.Evaluate(restockCount);

    public AnimationCurve restockPriceCurve;

    public void Awake()
    {
        shopDie = GameObject.Find("ShopDie");
        shopArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/BuyButton");
        allFaces = Resources.LoadAll<Face>("ScriptableObjects/Faces");
        soldOut = Resources.Load<Texture2D>("SoldOut"); // Change this make sure it exists AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
    }

    public void Start()
    {
        Restock();
    }

    public void Restock()
    {
        randomFaces = allFaces.OrderBy(x => Random.Range(-1f,1f)).Take(6).ToArray();

        for (int i = 0; i < shopArea.transform.childCount; i++)
        {
            Destroy(shopArea.transform.GetChild(i).gameObject);
        }


        foreach (Face face in randomFaces)
        {
            GameObject instance = Instantiate(buyButton, shopArea.transform);

            instance.GetComponent<Image>().sprite = Sprite.Create(face.Texture, new Rect(0, 0, face.Texture.width, face.Texture.height), new Vector2(0.5f, 0.5f));
            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(string.Format(format, face.price));


            // What the button does when clicked
            instance.GetComponent<Button>().onClick.AddListener(() => shopChangeFace.ChangeFace(face));
            instance.GetComponent<Button>().onClick.AddListener(() => shopDie.GetComponent<FaceChange>().UpdateDiceFaces());

            instance.GetComponent<Button>().onClick.AddListener(() => instance.GetComponent<Button>().interactable = false);
            Debug.Log("Added listeners to button for face: " + face.name);
            instance.GetComponent<Button>().onClick.AddListener(() => instance.GetComponentInChildren<TMPro.TMP_Text>().fontSize = 20);
            instance.GetComponent<Button>().onClick.AddListener(() => instance.GetComponentInChildren<TMPro.TMP_Text>().SetText("Sold Out"));
        }

    }
    
    public void PurchaseRestock()
    {
        if (FindAnyObjectByType<DiceScoreCalc>().points < RestockPrice)
            return;

        FindAnyObjectByType<DiceScoreCalc>().points -= RestockPrice;// Ask derek he has all the time in the world to explain this line to you, he loves doing that

        Restock();
        ++restockCount;
    }


    public void ResetValues()
    {
        restockCount = 0;
    }


}
