using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Die_Selection : MonoBehaviour
{
    public GameObject buyButton, selectionArea;
    public FaceChange[] faceChange;
    //public AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face> allDice = new(); // cahnge to the pablo inventory
    //private GlobalDie[] randomDice;
    private Run currentRun;

    [SerializeField] private GameObject shopDie; //to make people select a dice before they buy stuff :skull:
    [SerializeField] private GameObject selectDietext;

    [SerializeField] private List<RenderTexture> diceTextures;



    public void Awake()
    {
        selectionArea = this.gameObject;
        buyButton = Resources.Load<GameObject>("Prefabs/DieSelectButton");

        faceChange = FindObjectsByType<FaceChange>(FindObjectsSortMode.None).OrderBy(die => die.name).ToArray();
        //allDice = SaveDataController.Instance.current.run.De();

        shopDie = GameObject.FindGameObjectWithTag("ShopDie");
        selectDietext = GameObject.FindGameObjectWithTag("SelectDieText");
    }

    void Start()
    {
        Debug.Log("started");
        currentRun = SaveDataController.Instance.current.run;
        Debug.Log("got the save data");

        loadDice();
        Debug.Log("loaded dice");

        shopDie.SetActive(false);
    }

    public void loadDice()
    {
        for (int i = 0; i < selectionArea.transform.childCount; i++)
        {
            Destroy(selectionArea.transform.GetChild(i).gameObject);
            Debug.Log("Destroyed child " + i);
        }



        for (int i = 0; i < currentRun.Deese.Count; i++)
        {
            GameObject instance = Instantiate(buyButton, selectionArea.transform);            
            instance.name = "Die " + (i + 1);
            instance.AddComponent<GlobalDie>();
            instance.AddComponent<DiceDragging>();
            //instance.AddComponent<FaceChange>();
            RawImage image = instance.GetComponent<RawImage>();
            GlobalDie die = instance.GetComponent<GlobalDie>();
            DiceDragging texture = instance.GetComponent<DiceDragging>();
            die.Faces = currentRun.Deese[i];//
            //FaceChange face = faceChange[i];

            image.texture = diceTextures[i];
            texture.diceTF = GameObject.Find("DV " + (i + 1)).transform;
            texture.cameraIndex = i;
            texture.diceTexture = image;


            //instance.GetComponentInChildren<TMPro.TMP_Text>().SetText(die.name);

            instance.GetComponent<Button>().onClick.AddListener(() => ChangeDie(die, /*face,*/ shopDie, die.gameObject));
            instance.GetComponent<Button>().onClick.AddListener(() => shopDie.SetActive(true));
            instance.GetComponent<Button>().onClick.AddListener(() => selectDietext.SetActive(false));

            instance.GetComponent<Button>().onClick.AddListener(() => Debug.Log("clicky clicky"));

            //instance.GetComponent<Button>().onClick.AddListener(() => die.gameObject.GetComponent<FaceChange>().UpdateDiceFaces());
        }
        
    }

    public void ChangeDie(GlobalDie newDie, /*FaceChange faceChange, */GameObject shopdie, GameObject dieToUpdate)
    {
        // Change all refereces to Global die to the new die
        //Debug.Log(faceChange.gameObject.name);

        shopdie.GetComponent<FaceChange>().Dice = newDie;
        shopdie.GetComponent<FaceChange>().UpdateDiceFaces();
        shopdie.GetComponent<GlobalDie>().Faces = newDie.Faces;
        //dieToUpdate.GetComponent<FaceChange>().UpdateDiceFaces();

    }
}
