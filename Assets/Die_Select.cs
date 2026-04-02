using UnityEngine;
using UnityEngine.UI;

public class Die_Select : MonoBehaviour
{
    public ShopChangeFace shopChangeFace;
    public FaceChange faceChange;

    public GlobalDie[] dice;

    public GameObject diceSelection, buyButton;

    public void Awake()
    {
        diceSelection = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/BuyButton");

        shopChangeFace = FindAnyObjectByType<ShopChangeFace>();
        faceChange = FindAnyObjectByType<FaceChange>();
        dice = FindObjectsByType<GlobalDie>(FindObjectsSortMode.None);
    }

    public void Start()
    {

        foreach (GlobalDie die in dice)
        {
            GameObject instance = Instantiate(buyButton, diceSelection.transform);


            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(die.name);


            instance.GetComponent<Button>().onClick.AddListener(() => ChangeDie(die));
            instance.GetComponent<Button>().onClick.AddListener(() => faceChange.UpdateDiceFaces());
        }
    }
    public void ChangeDie(GlobalDie newDie)
    { 
        // Change all refereces to Global die to the new die
        FaceChange faceChange = FindAnyObjectByType<FaceChange>();
        faceChange.Dice = newDie;


    }
}
