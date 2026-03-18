using UnityEngine;
using UnityEngine.EventSystems;


public class DiceVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    public int boxIndex;
    public DiceDragging currentDice; //The dice in this slot

    private DiceHolder holder;

    void Start()
    {
        holder = DiceHolder.Instance;

        if (transform.childCount > 0)
        {
            currentDice = transform.GetChild(0).GetComponent<DiceDragging>();
            currentDice.SetSlot(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        holder.hoveredSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        holder.hoveredSlot = null;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (holder.hoveredSlot == this && currentDice != null)
        {
            float rotX = eventData.delta.y; 
            float rotY = eventData.delta.x; 
            float rotZ = eventData.delta.x; //change this later
            currentDice.diceTF.Rotate(rotX, rotY, rotZ, Space.World); 
        }
    }

    public void PlaceDice(DiceDragging dice)
    {

        // if (dice.GetSlot() == this)
        // {
        //     dice.SetSlot(this);
        //     Debug.Log("idk");
        //     return;
        // }

        if (currentDice == null)
        {
            currentDice = dice;
            dice.SetSlot(this);
            Debug.Log("Placing in an empty slot");

        }
        else
        {
            DiceDragging other = currentDice;
            DiceVisual oldSlot = dice.GetSlot();

            currentDice = dice;
            dice.SetSlot(this);

            oldSlot.currentDice = other;
            other.SetSlot(oldSlot);
            Debug.Log($"Placing {dice.gameObject.name} in {this.gameObject.name} and {other.gameObject.name} in {oldSlot.gameObject.name}");
        }
    }
    

}
