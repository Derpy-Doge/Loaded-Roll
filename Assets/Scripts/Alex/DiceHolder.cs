using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DiceHolder : MonoBehaviour //test
{
    public static DiceHolder Instance;
    [HideInInspector] public DiceVisual hoveredSlot;
    private DiceVisual originalSlot;
    private DiceDragging heldDice;
    [SerializeField] private GameObject draggingLayer;
    private GameObject diceTexturePrefab;
    [SerializeField] private DiceVisual inventorySlot; //These could probably be static and set in the dicevisual start function
    [SerializeField] private DiceVisual recycleSlot; //These could probably be static and set in the dicevisual start function
    [SerializeField] private DiceVisual[] hotbar = new DiceVisual[5];
    [HideInInspector] public float GlowSpeed;

    //Materials
    [HideInInspector] public Material glow;
    private Material purpleGlow;
    private Material purpleFullGlow;
    private float customTime;




    void Awake()
    {
        Instance = this;
        glow = Resources.Load<Material>("Materials/NewGlow");
        purpleFullGlow = Resources.Load<Material>("Materials/NewFullGlow");
        purpleGlow = Resources.Load<Material>("Materials/NewPurple");
        diceTexturePrefab = Resources.Load<GameObject>("Prefabs/diceTexture");

    }
    

    void Update()
    {
        if (glow != null) 
        {
            GlowSpeed = Mathf.Max(0f, GlowSpeed - Time.deltaTime * 0.4f);
            GlowSpeed = Mathf.Clamp(GlowSpeed, 0f, 4.7f);
            //Debug.Log($"GS: {GlowSpeed}");

            customTime += Time.deltaTime * 0.2f * (1 + GlowSpeed);

            glow.SetFloat("_AnimationSpeed", customTime);
            purpleFullGlow.SetFloat("_AnimationSpeed", customTime);
            purpleGlow.SetFloat("_AnimationSpeed", customTime);
        }
    }

    public RectTransform GetDraggingObj()
    {
        return draggingLayer.GetComponent<RectTransform>();
    }

    public DiceDragging GetHeld()
    {
        return heldDice;
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }

        int state = (int) GameManager.Instance.CurrentState;

        if (state == (int) GameManager.GameStates.Rolling)
        {
            if (heldDice != null)
            {
                if (hoveredSlot != null)
                {
                    //Debug.Log("SLOT FOUND");
                    hoveredSlot.PlaceDice(heldDice);
                    if (hoveredSlot.GetStorageType() == DiceVisual.StorageType.Inventory)
                    {
                        heldDice = null;
                        originalSlot = null;
                        return;
                    }
                }
                else
                {
                    //Debug.Log("NO SLOT FOUND");
                    if (originalSlot == null) //Means dice comes from inventory (hopefully)
                    {
                        inventorySlot.PlaceDice(heldDice);
                    }
                    else
                    {
                        heldDice.returning = true;
                        originalSlot.PlaceDice(heldDice);
                    }
                }

                heldDice.StopDragging();
                heldDice = null;
                originalSlot = null;
            }
            else
            {
                if (hoveredSlot != null && hoveredSlot.currentDice != null && hoveredSlot.currentDice.selectable)
                {
                    heldDice = hoveredSlot.currentDice;
                    originalSlot = hoveredSlot;
                    hoveredSlot.currentDice = null;

                    heldDice.Dragging = true;
                    heldDice.transform.SetParent(draggingLayer.transform);
                    //Debug.Log($"PICKING UP {heldDice.gameObject.name} FROM {hoveredSlot.gameObject.name}");
                }
            }
        }
        else if (state == (int) GameManager.GameStates.Select)
        {
            if (hoveredSlot != null) //other
            {
                
                if (hoveredSlot.GetStorageType() == DiceVisual.StorageType.Inventory)
                {
                    return;
                }


                if (hoveredSlot.selected)
                {
                    if (!hoveredSlot.currentDice.selectable) //Means the user has already selected the dice in a previous roll so it cant be unselected
                    {
                        return;
                    }

                    RawImage rI = hoveredSlot.currentDice.GetComponent<RawImage>();
                    RollDice.Instance.Selected[hoveredSlot.boxIndex] = null;
                    RollDice.Instance.UnselectedDice.Add(rI);
                    RollDice.Instance.UnselectedSlot.Add(hoveredSlot.currentDice);
                    RollDice.Instance.dices[hoveredSlot.boxIndex].gameObject.layer = LayerMask.NameToLayer("Default");
                    hoveredSlot.selected = false;
                    rI.material = glow;
                    RollDice.Instance.resultFaces[hoveredSlot.boxIndex].material = null;

                }
                else
                {
                    RawImage rI = hoveredSlot.currentDice.GetComponent<RawImage>();
                    hoveredSlot.selected = true;
                    RollDice.Instance.dices[hoveredSlot.boxIndex].gameObject.layer = LayerMask.NameToLayer("Selected");
                    RollDice.Instance.Selected[hoveredSlot.boxIndex] = hoveredSlot.currentDice;
                    RollDice.Instance.UnselectedDice.Remove(rI);
                    RollDice.Instance.UnselectedSlot.Remove(hoveredSlot.currentDice);
                    rI.material = purpleGlow;
                    RollDice.Instance.resultFaces[hoveredSlot.boxIndex].material = purpleFullGlow;
                    
                }
                //Debug.Log("test");
            }
            
        }

    }

    public void RecycleDice(DiceDragging diceDragging)
    {
        recycleSlot.recyclingDice.Add(diceDragging);
        diceDragging.transform.SetParent(recycleSlot.transform);
        diceDragging.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void EmptyRecycle()
    {
        recycleSlot.EmptyDice();
    }

    public void CreateDice(GlobalDie die, GameObject visualReference, int index)
    {
        GameObject newDie =  Instantiate(diceTexturePrefab, draggingLayer.transform);
        newDie.GetComponent<RawImage>().texture = visualReference.transform.GetChild(1).GetComponent<Camera>().targetTexture;
        newDie.GetComponent<RawImage>().material = glow;
        DiceDragging diceDragging = newDie.GetComponent<DiceDragging>();
        diceDragging.diceTF = visualReference.transform.GetChild(0);
        diceDragging.diceTF.GetComponent<FaceChange>().Dice = die; //When we change GlobalDie to not be a scriptable object this will need changed too
        diceDragging.diceTF.GetComponent<FaceChange>().UpdateDiceFaces();
        diceDragging.cameraIndex = index;
        diceDragging.SetSlot(inventorySlot);
        heldDice = diceDragging; //DiceDragging
        originalSlot = null; 

        heldDice.Dragging = true;
        //heldDice.transform.SetParent(draggingLayer.transform);
    }

    public void Fill(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            QuickFill();
        }
    }

    public void QuickFill()
    {
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].currentDice == null && hotbar[i] != originalSlot)
            {
                
            }
        }
    }

}
