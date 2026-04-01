using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DiceHolder : MonoBehaviour
{
    public static DiceHolder Instance;
    [HideInInspector] public DiceVisual hoveredSlot;
    private DiceVisual originalSlot;
    private DiceDragging heldDice;
    [SerializeField] private GameObject draggingLayer;
    private GameObject diceTexturePrefab;
    [SerializeField] private DiceVisual inventorySlot;

    //Materials
    public Material glow;
    private Material purpleGlow;
    private Material purpleFullGlow;




    void Awake()
    {
        Instance = this;
        glow = Resources.Load<Material>("Materials/Glow");
        purpleFullGlow = Resources.Load<Material>("Materials/FullGlow");
        purpleGlow = Resources.Load<Material>("Materials/PurpleGlow");
        diceTexturePrefab = Resources.Load<GameObject>("Prefabs/diceTexture");

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
                if (hoveredSlot != null && hoveredSlot.currentDice != null)
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
                    RawImage rI = hoveredSlot.currentDice.GetComponent<RawImage>();
                    RollDice.Instance.UnselectedDice.Add(rI);
                    hoveredSlot.selected = false;
                    rI.material = glow;
                    RollDice.Instance.resultFaces[hoveredSlot.boxIndex].material = null;

                }
                else
                {
                    RawImage rI = hoveredSlot.currentDice.GetComponent<RawImage>();
                    hoveredSlot.selected = true;
                    RollDice.Instance.UnselectedDice.Remove(rI);
                    rI.material = purpleGlow;
                    RollDice.Instance.resultFaces[hoveredSlot.boxIndex].material = purpleFullGlow;
                    
                }
                Debug.Log("test");
            }
            
        }

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

}
