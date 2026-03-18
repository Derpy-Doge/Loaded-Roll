using UnityEngine;
using UnityEngine.InputSystem;


public class DiceHolder : MonoBehaviour
{
    public static DiceHolder Instance;
    [HideInInspector] public DiceVisual hoveredSlot;
    private DiceVisual originalSlot;
    private DiceDragging heldDice;
    [SerializeField] private GameObject draggingLayer;


    void Awake()
    {
        Instance = this;
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }

        if (heldDice != null)
        {
            if (hoveredSlot != null)
            {
                Debug.Log("SLOT FOUND");
                hoveredSlot.PlaceDice(heldDice);
            }
            else
            {
                Debug.Log("NO SLOT FOUND");
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

                Debug.Log($"PICKING UP {heldDice.gameObject.name} FROM {hoveredSlot.gameObject.name}");
            }
        }
    }
}
