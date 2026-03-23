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
    public Material glow;
    private Material purpleGlow;


    void Awake()
    {
        Instance = this;
        glow = Resources.Load<Material>("Materials/Glow");
        purpleGlow = Resources.Load<Material>("Materials/PurpleGlow");

    }

    public void Click(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }

        int state = (int) GameManager.Instance.currentState;

        if (state == (int) GameManager.GameStates.Rolling)
        {
            if (heldDice != null)
            {
                if (hoveredSlot != null)
                {
                    //Debug.Log("SLOT FOUND");
                    hoveredSlot.PlaceDice(heldDice);
                }
                else
                {
                    //Debug.Log("NO SLOT FOUND");
                    heldDice.returning = true;
                    originalSlot.PlaceDice(heldDice);
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
                    hoveredSlot.selected = false;
                    hoveredSlot.currentDice.GetComponent<RawImage>().material = glow;
                    RollDice.Instance.resultFaces[hoveredSlot.boxIndex].material = glow;

                }
                else
                {
                    hoveredSlot.currentDice.GetComponent<RawImage>().material = purpleGlow;
                    RollDice.Instance.resultFaces[hoveredSlot.boxIndex].material = purpleGlow;
                    hoveredSlot.selected = true;
                    
                }
                Debug.Log("test");
            }
            
        }

    }
}
