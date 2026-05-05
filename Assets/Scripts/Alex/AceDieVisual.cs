using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class AceDieVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{


    public enum AceDie
    {
        Gamble = 0,
        Horse = 1,
        Interest = 3,
        Generic = 4,

    }

    public enum State
    {
        StartMenu = -1,
        Game = 1,    
    }

    [SerializeField] private Transform aceDiceTransform;
    [SerializeField] private FaceChange aceFaceChange;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float Sensitivy;
    private bool hovering;
    private string[] aceDieList;
    [SerializeField] private TMPro.TMP_Text title;
    [SerializeField] private TMPro.TMP_Text description;
    [SerializeField] private string[] titles;
    [SerializeField] [TextArea] private string[] descriptions;
    [SerializeField] private State currentState;
    public static AceDieVisual Instance;
    private List<AceDie> UnlockedDie = new();
    [SerializeField] private GameObject lockImage;
    [HideInInspector] public bool CanContinue = false;


    [SerializeField] private AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face> gambleDice;
    [SerializeField] private AYellowpaper.SerializedCollections.SerializedDictionary<Vector3, Face> horseDice;


    public AceDie currentAceDie = AceDie.Gamble;



    void Awake()
    {
        Instance = this;
        aceDieList = System.Enum.GetNames(typeof(AceDie));
    }

    void Start()
    {
        for (int i = 0; i < SaveDataController.Instance.current.DebtInstallmentsCompleted / 10 + 1; i++)
        {
            if (i == aceDieList.Length)
                break;

            UnlockedDie.Add((AceDie) i);
        }

        if (currentState == State.Game)
        {
            currentAceDie = (AceDie) SaveDataController.Instance.current.run.AceDie;
            switch (currentAceDie)
            {
                case AceDie.Gamble:
                    aceFaceChange.Dice.Faces = gambleDice;
                    break;

                case AceDie.Horse:
                    aceFaceChange.Dice.Faces = horseDice;
                    break;
            }
            aceFaceChange.UpdateDiceFaces();
            Debug.Log("AAAAAAAAAAAAAAAAAA");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //GetComponent<RawImage>().material = holder.glow;
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        
        GetComponent<RawImage>().material = null;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!hovering)
            return;
            
        // float rotX = Mathf.Log(1f + eventData.delta.y, 2f); 
        // float rotY = Mathf.Log(1f + eventData.delta.x, 2f); 
        // float rotZ = -Mathf.Log(1f + eventData.delta.x, 2f);  //change this later

        float rotX = Mathf.Sign(eventData.delta.y) * Mathf.Log(1f + Mathf.Abs(eventData.delta.y));
        float rotY = Mathf.Sign(eventData.delta.x) * Mathf.Log(1f + Mathf.Abs(eventData.delta.x));
        float rotZ = -rotY;


        Debug.Log(new Vector3(rotX, rotY, rotZ));
        rb.angularVelocity += new Vector3(rotX, rotY, rotZ) * Sensitivy;
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 10f);
        //aceDiceTransform.Rotate(rotX, rotY, rotZ, Space.World); 
    }

    public void SwitchAceDie(int dir)
    {
        if (currentState == State.Game)
        {
            return;
        }

        int count = aceDieList.Length; //Change this to its own variable later rather than making a new one every call of func
        int newIndex = ((int) currentAceDie + dir) % count;

        if (newIndex < 0)
        {
            newIndex += count;
        }

        currentAceDie = (AceDie)newIndex;
        switch (currentAceDie)
        {
            case AceDie.Gamble:
                aceFaceChange.Dice.Faces = gambleDice;
                break;
            
            case AceDie.Horse:
                aceFaceChange.Dice.Faces = horseDice;
                break;
        }
        if (UnlockedDie.Contains(currentAceDie))
        {
            CanContinue = true;
            lockImage.SetActive(false);
        }
        else
        {
            CanContinue = false;
            lockImage.SetActive(true);
        }
        title.text = titles[(int) currentAceDie];
        description.text = descriptions[(int) currentAceDie];
        aceFaceChange.UpdateDiceFaces();
    } 


    public void UpdateTextBoxes()
    {
        if (currentState == State.Game)
        {
            return;
        }

        if (currentAceDie == AceDie.Gamble)
        {
            CanContinue = true;
        }

        title.text = titles[(int) currentAceDie];
        description.text = descriptions[(int) currentAceDie];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
