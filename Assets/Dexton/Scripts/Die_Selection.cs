using UnityEngine;
using UnityEngine.UI;

public class Die_Selection : MonoBehaviour
{
    private int _flib = 0;

    public GameObject buyButton, selectionArea;
    public FaceChange[] faceChange;
    public GlobalDie[] dice;

    public void Awake()
    {
        selectionArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/BuyButton");

        faceChange = FindObjectsByType<FaceChange>(FindObjectsSortMode.None);
        dice = FindObjectsByType<GlobalDie>(FindObjectsSortMode.None);
    }

    void Start()
    {
        foreach (GlobalDie die in dice)
        {
            GameObject instance = Instantiate(buyButton, selectionArea.transform);

            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(die.name);
            instance.GetComponent<Button>().onClick.AddListener(() => ChangeDie(die));
            instance.GetComponent<Button>().onClick.AddListener(() => faceChange[_flib].UpdateDiceFaces());

            _flib++;
        }
    }

    public void ChangeDie(GlobalDie newDie)
    {
        // Change all refereces to Global die to the new die
        FaceChange faceChange = this.gameObject.GetComponent<FaceChange>();
        Debug.Log(faceChange.gameObject.name);
        faceChange.Dice = newDie;
    }
}
