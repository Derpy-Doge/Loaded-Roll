using UnityEngine;
using UnityEngine.UI;

public class Die_Select : MonoBehaviour
{
    public ShopChangeFace shopChangeFace;
    public GlobalDie[] dice;

    public GameObject shopArea, buyButton;

    public void Awake()
    {
        shopArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/BuyButton");
        dice = Resources.LoadAll<GlobalDie>("ScriptableObjects/Dice");
    }

    public void Start()
    {
        foreach (GlobalDie face in dice)
        {
            GameObject instance = Instantiate(buyButton, shopArea.transform);

            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(face.name);
            //instance.GetComponent<Button>().onClick.AddListener(() => shopChangeFace.ChangeFace(face));
            //instance.GetComponent<Button>().onClick.AddListener(() => shopChangeFace.ChangeFace(face));
        }
    }
}
