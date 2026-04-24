using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Die_Selection : MonoBehaviour
{
    public GameObject buyButton, selectionArea;
    public FaceChange[] faceChange;
    public GlobalDie[] allDice; // cahnge to the pablo inventory
    private GlobalDie[] randomDice;


    public void Awake()
    {
        selectionArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/DieSelectButton");

        faceChange = FindObjectsByType<FaceChange>(FindObjectsSortMode.None).OrderBy(die => die.name).ToArray();
        allDice = FindObjectsByType<GlobalDie>(FindObjectsSortMode.None).OrderBy(die => die.name).ToArray();
    }

    void Start()
    {
        loadDice();
    }

    public void loadDice()
    {
        for (int i = 0; i < selectionArea.transform.childCount; i++)
        {
            Destroy(selectionArea.transform.GetChild(i).gameObject);
        }

        randomDice = allDice.OrderBy(x => Random.Range(-1f, 1f)).Take(6).ToArray();


        for (int i = 0; i < randomDice.Length; i++)
        {
            GameObject instance = Instantiate(buyButton, selectionArea.transform);
            GlobalDie die = randomDice[i];
            FaceChange face = faceChange[i];

            instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(die.name);

            instance.GetComponent<Button>().onClick.AddListener(() => ChangeDie(die, face, GameObject.Find("ShopDie"), die.gameObject));

            instance.GetComponent<Button>().onClick.AddListener(() => die.gameObject.GetComponent<FaceChange>().UpdateDiceFaces());
        }
        
    }

    public void ChangeDie(GlobalDie newDie, FaceChange faceChange, GameObject shopdie, GameObject dieToUpdate)
    {
        // Change all refereces to Global die to the new die
        Debug.Log(faceChange.gameObject.name);

        shopdie.GetComponent<FaceChange>().Dice = newDie;
        shopdie.GetComponent<FaceChange>().UpdateDiceFaces();
        shopdie.GetComponent<GlobalDie>().Faces = newDie.Faces;
        //dieToUpdate.GetComponent<FaceChange>().UpdateDiceFaces();

    }
}
