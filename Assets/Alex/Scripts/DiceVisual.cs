using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class DiceVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    public enum StorageType
    {
        Inventory = 0,
        Hotbar = 1,
    }

    

    public int boxIndex;
    public DiceDragging currentDice; //The dice in this slot

    [HideInInspector] public bool selected;
    [SerializeField] StorageType storageType;

    private DiceHolder holder;

    void Start()
    {
        holder = DiceHolder.Instance;

        if (transform.childCount > 0)
        {
            if (storageType == StorageType.Hotbar)
            {
                currentDice = transform.GetChild(0).GetComponent<DiceDragging>();
                currentDice.SetSlot(this);
                RollDice.Instance.diceTextures[boxIndex] = currentDice.visualFC.Dice;
                
            }
            
        }
    }

    public StorageType GetStorageType()
    {
        return storageType;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        holder.hoveredSlot = this;
        if (currentDice != null && !selected)
        {
            currentDice.GetComponent<RawImage>().material = holder.glow;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        holder.hoveredSlot = null;
        if (currentDice != null && !currentDice.Dragging && !selected)
        {
            currentDice.GetComponent<RawImage>().material = null;
        }
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
        if (currentDice == null)
        {
            if (storageType == StorageType.Inventory && dice.GetSlot().storageType == StorageType.Inventory) //Moving dice from inventory slot to empty inventory slot
            {
                //lowkey think nothing needs done here
            }
            else if (storageType == StorageType.Hotbar && dice.GetSlot().storageType == StorageType.Hotbar) //Moving dice from hotbar slot to empty hotbar slot
            {
                RollDice.Instance.diceTextures.Swap(boxIndex, dice.GetSlot().boxIndex);
            }
            else if (storageType == StorageType.Inventory) //Moving dice from hotbar slot to empty inventory slot
            {
                Inventory.Instance.PlaceDice(dice);  
                RollDice.Instance.diceTextures[dice.GetSlot().boxIndex] = null;
                dice.SetSlot(StorageType.Inventory);
                return;
            }
            else //Moving from inventory slot to empty hotbar slot
            {
                Debug.Log($"Box Index: {boxIndex}");
                RollDice.Instance.diceTextures[boxIndex] = dice.visualFC.Dice;
                
            }
        
            currentDice = dice;
            dice.SetSlot(this);
            //Debug.Log("Placing in an empty slot");

        }
        else
        {
            DiceDragging other = currentDice;
            DiceVisual oldSlot = dice.GetSlot();

            currentDice = dice;
            dice.SetSlot(this);

            oldSlot.currentDice = other;
            other.SetSlot(oldSlot);

            if (oldSlot.storageType == StorageType.Inventory && storageType == StorageType.Inventory) //Moving dice from inventory slot to inventory slot
            {
                Debug.Log("Moving dice from inventory slot to inventory slot");
            }
            else if (oldSlot.storageType == StorageType.Hotbar && storageType == StorageType.Hotbar) //Moving dice from hotbar slot to hotbar slot
            {
                Debug.Log("Moving dice from hotbar slot to hotbar slot");
                RollDice.Instance.diceTextures.Swap(boxIndex, oldSlot.boxIndex);


            }
            else if (oldSlot.storageType == StorageType.Inventory) //Moved from inventory to hotbar
            {
                Debug.Log("Moving dice from inventory to hotbar");
                RollDice.Instance.diceTextures[boxIndex] = currentDice.visualFC.Dice;
            }
            else //Moved from hotbar to inventory 
            {
                Debug.Log("Moving dice from hotbar to inventory");
                RollDice.Instance.diceTextures[oldSlot.boxIndex] = other.visualFC.Dice;

                
            }

            other.GetComponent<RawImage>().material = null;
            //Debug.Log($"Placing {dice.gameObject.name} in {this.gameObject.name} and {other.gameObject.name} in {oldSlot.gameObject.name}");
        }
    }
    

}
