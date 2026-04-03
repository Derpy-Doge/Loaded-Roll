using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class RollDice : MonoBehaviour
{
    private Rigidbody rb;
    public List<GlobalDie> diceTextures = new () {null, null, null, null, null};
    [SerializeField] private float diceSpinCooldown;
    [SerializeField] private Transform diceCamera;
    [SerializeField] private bool follow;
    [SerializeField] private RectTransform arcReference;

    [SerializeField] private List<Transform> dices = new();
    private Dictionary<Transform, Rigidbody> diceRB = new();
    private bool calculated = true;
    private float timeSinceCalc;
    private float calcCooldown = .25f;
    private GameManager gameManager;
    private int nextDiceRoll = 5;

    private bool ignoreSelectWarning; //should likely have this in save data
    private bool selectWarningSpawned;



    public static RollDice Instance;

    public List<RawImage> resultFaces = new ();
    [HideInInspector] public List<Face> rolledFaces = new ();
    public List<RawImage> UnselectedDice = new();
    public List<DiceDragging> UnselectedSlot = new();
    public DiceDragging[] AllSlots = new DiceDragging[5]; 
    public RawImage[] AllDice = new RawImage[5]; //When i have more time remove this in place of using allslots


    void Start()
    {

        if (Instance != null)
        {
            Debug.LogError("There is multiple RollDice scripts in this scene and it will not function properly");
        }

        Instance = this;
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            diceRB.Add(dices[i], dices[i].GetComponent<Rigidbody>());
        }
        rb = gameObject.GetComponent<Rigidbody>();
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("FaceResults"))
        {
            resultFaces.Add(gO.GetComponent<RawImage>());
        } 
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        timeSinceCalc -= Time.deltaTime;
        if (!calculated && timeSinceCalc <= 0f && CalculateSpeed() < .2f)
        {
            calculated = true;
            ReadFaces();
        }
    }

    public void AHHAGBAH()
    {   
        if (gameManager.CurrentState == GameManager.GameStates.Select)
        {
            if (UnselectedDice.Count == 5 && !ignoreSelectWarning)
            {
                if (selectWarningSpawned)
                {
                    //Means the user pressed the select button again after the warning, either like shake the warning or just assume its the same as press okay but not dont show again
                }
                else
                {
                    selectWarningSpawned = true;
                    //play a warning here
                    
                }
            }
            else
            {
                StartCoroutine(Select()); 
            }
            return;
        }


        if (gameManager.Roll())
        {
            StartCoroutine(Roll());
        }

    }

    private float CalculateSpeed()
    {
        timeSinceCalc = calcCooldown;
        float speed = 0f;

        foreach (var dice in dices)
        {
            speed += diceRB[dice].linearVelocity.magnitude;
        }

        return speed;
    }

    IEnumerator Select()
    {
        gameManager.CurrentState = GameManager.GameStates.Busy;
        int count = UnselectedDice.Count;
        if (count == 0) //Means all dice are selected //Run calculation here.
        {
            nextDiceRoll = 5;
            
        }
        else
        {
            nextDiceRoll = count;
            //Animation Stuff
            //-----------------------------------------------------------------------------------
            float time = 0f;
            float duration = .8f;
            List<Vector2> startPositions = new();
            Vector2 endPosition; // = arcReference.anchoredPosition;
            List<RectTransform> rTS = new();
            List<Transform> orgParents = new();
            RectTransform dragging = DiceHolder.Instance.GetDraggingObj();
            for (int i = 0; i < count; i++)
            {
                rTS.Add(UnselectedDice[i].gameObject.GetComponent<RectTransform>());
                UnselectedDice[i].transform.SetParent(dragging);
                startPositions.Add(rTS[i].anchoredPosition);  
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rTS[0].parent as RectTransform, RectTransformUtility.WorldToScreenPoint(null, arcReference.position), null, out endPosition);

            while (time < duration)
            {
                Debug.Log("test");
                time += Time.deltaTime;
                float t = time / duration;
                for (int i = 0; i < count; i++)
                {
                    Vector2 pos = Vector2.Lerp(startPositions[i], new Vector2 (endPosition.x - (2 - i) * 50, endPosition.y), t);

                    float height = 330 * 4 * (t- t * t);
                    rTS[i].anchoredPosition = pos + Vector2.up * height;
                }
                yield return null;
            }

            for (int i = 0; i < count; i++)
            {
                rTS[i].anchoredPosition = new Vector2 (endPosition.x - (2 - i) * 50, endPosition.y);
            }
            //-----------------------------------------------------------------------------------

            //Transfer the dice to a recycle slot
            for (int i = 0; i < count; i++)
            {
                //UnselectedSlot[i]
                DiceHolder.Instance.RecycleDice(UnselectedSlot[i]);
            }
            gameManager.SwapInventory();
        }

        yield break;
    }

    //private void 

    IEnumerator Roll() //Make this seeded
    {
        Debug.Log(UnselectedSlot.Count);
        Debug.Log(UnselectedDice.Count);
        rolledFaces.Clear();
        for (int i = 0; i < nextDiceRoll; i++)
        {
            dices[i].GetComponent<FaceChange>().Dice = diceTextures[i];
            dices[i].GetComponent<FaceChange>().UpdateDiceFaces();
            dices[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            dices[i].position = new Vector3(i - 2f, -2f, i - 2f);
            dices[i].Rotate(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
            diceRB[dices[i]].linearVelocity = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
            diceRB[dices[i]].angularVelocity = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-8f, 8f));
            calculated = false;
            yield return new WaitForSeconds(diceSpinCooldown);
        }


    }

    [ContextMenu("Read Faces")]
    private void ReadFaces()
    {
        UnselectedSlot = AllSlots.ToList();
        for (int i = 0; i < AllSlots.Length; i++)
        {
            AllDice[i] = AllSlots[i].diceTexture;
        }
        UnselectedDice = AllDice.ToList();


        for (int i = 0; i < dices.Count; i++)
        {
            Dictionary<Vector3, Face> sides = new()
            {
                [-dices[i].transform.up] = diceTextures[i].Faces[Vector3.down],
                [dices[i].transform.up] = diceTextures[i].Faces[Vector3.up],
                [-dices[i].transform.right] = diceTextures[i].Faces[Vector3.left],
                [dices[i].transform.right] = diceTextures[i].Faces[Vector3.right],
                [-dices[i].transform.forward] = diceTextures[i].Faces[Vector3.back],
                [dices[i].transform.forward] = diceTextures[i].Faces[Vector3.forward]
            };

            var ordered = sides.Select(item => item.Key).OrderByDescending(item => Vector3.Dot(item, Vector3.up));

            rolledFaces.Add(sides[ordered.FirstOrDefault()]);
            int num = rolledFaces[i].pips;
            sides[ordered.FirstOrDefault()].Effect.Invoke();
            resultFaces[i].texture = rolledFaces[i].Texture;
            Debug.Log(num);
            gameManager.CurrentState = GameManager.GameStates.Select;
            gameManager.SwapStateButton();
        }
    }
}
