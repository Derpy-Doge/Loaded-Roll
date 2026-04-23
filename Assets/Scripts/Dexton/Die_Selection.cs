using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Die_Selection : MonoBehaviour
{
    public GameObject buyButton, selectionArea;
    public FaceChange[] faceChange;
    public GlobalDie[] dice;

    public void Awake()
    {
        selectionArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/DieSelectButton");

        faceChange = FindObjectsByType<FaceChange>(FindObjectsSortMode.None).OrderBy(die => die.name).ToArray();
        dice = FindObjectsByType<GlobalDie>(FindObjectsSortMode.None).OrderBy(die => die.name).ToArray();
    }

    void Start()
    {
        for (int i = 0; i < dice.Length; i++)
        {
            GameObject instance = Instantiate(buyButton, selectionArea.transform);
            GlobalDie die = dice[i];
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
