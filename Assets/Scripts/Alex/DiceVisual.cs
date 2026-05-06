using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class DiceVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    public enum StorageType
    {
        Inventory = 0,
        Hotbar = 1,
        Recycle = 2,
    }

    

    public int boxIndex;
    public DiceDragging currentDice; //The dice in this slot

    [HideInInspector]  public bool selected; 

    [HideInInspector] public List<DiceDragging> recyclingDice = new();
    [SerializeField] StorageType storageType;

    private DiceHolder holder;
    private Inventory inventory;

    void Start()
    {
        holder = DiceHolder.Instance;
        inventory = Inventory.Instance;
        if (transform.childCount > 0)
        {
            if (storageType == StorageType.Hotbar)
            {
                currentDice = transform.GetChild(0).GetComponent<DiceDragging>();
                currentDice.SetSlot(this);
                RollDice.Instance.diceTextures[boxIndex] = currentDice.visualFC.Dice;
                //RollDice.Instance.AllDice[boxIndex] = currentDice.diceTexture;
                RollDice.Instance.AllSlots[boxIndex] = currentDice;

                
            }
            
        }
    }

    public StorageType GetStorageType()
    {
        return storageType;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (storageType == StorageType.Recycle)
        {
            return;
        }

        holder.hoveredSlot = this;
        if (currentDice != null && !selected)
        {
            currentDice.GetComponent<RawImage>().material = holder.glow;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (storageType == StorageType.Recycle)
        {
            return;
        }
        //holder.GlowSpeed = 0f;
        holder.hoveredSlot = null;
        if (currentDice != null && !currentDice.Dragging && !selected)
        {
            currentDice.GetComponent<RawImage>().material = null;
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (storageType == StorageType.Recycle)
        {
            return;
        }

        if (holder.hoveredSlot == this && currentDice != null)
        {
            float rotX = eventData.delta.y; 
            float rotY = eventData.delta.x; 
            float rotZ = -eventData.delta.x; //change this later
            holder.GlowSpeed += Mathf.Clamp(Mathf.Sqrt(eventData.delta.magnitude) * .005f, 0, 5);
            //currentDice.diceTF.Rotate(rotX, rotY, rotZ, Space.World); 
        }
    }

    public void EmptyDice()
    {
        if (storageType != StorageType.Recycle)
        {
            Debug.LogError($"A hotbar slot, {gameObject.name}, is trying to run the function EmptyDice() while not being a recycling slot.");
            return;
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<DiceDragging>(out DiceDragging diceD))
            {
                inventory.PlaceDice(diceD, true);  
            }
            
        }
    }

    public void ExecuteDice()
    {
        if (storageType != StorageType.Recycle)
        {
            Debug.LogError($"A hotbar slot, {gameObject.name}, is trying to run the function ExecuteDice() while not being a recycling slot.");
            return;


        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<DiceDragging>(out DiceDragging diceD))
            {
                Destroy(diceD.gameObject);
            }
            
        }

    }

    public void PlaceDice(DiceDragging dice)
    {
        if (currentDice == null)
        {

            if (storageType == StorageType.Inventory && dice.GetSlot().storageType == StorageType.Inventory) //Moving dice from inventory slot to empty inventory slot
            {
                inventory.PlaceDice(dice, true);  
                dice.SetSlot(StorageType.Inventory); //I think this is pointless

                //So this is moving from inventory to non enmpty hotbar for some reason
                return;


            }
            else if (storageType == StorageType.Hotbar && dice.GetSlot().storageType == StorageType.Hotbar) //Moving dice from hotbar slot to empty hotbar slot
            {
                RollDice.Instance.diceTextures.Swap(boxIndex, dice.GetSlot().boxIndex);
                //RollDice.Instance.AllDice.Swap(boxIndex, dice.GetSlot().boxIndex); 
                RollDice.Instance.AllSlots.Swap(boxIndex, dice.GetSlot().boxIndex); 




            }
            else if (storageType == StorageType.Inventory) //Moving dice from hotbar slot to empty inventory slot
            {
                inventory.PlaceDice(dice, false);  
                RollDice.Instance.diceTextures[dice.GetSlot().boxIndex] = null;
                //RollDice.Instance.AllDice[dice.GetSlot().boxIndex] = null;
                RollDice.Instance.AllSlots[dice.GetSlot().boxIndex] = null;



                dice.SetSlot(StorageType.Inventory); //I think this is pointless
                return;
            }
            else //Moving from inventory slot to empty hotbar slot //putting in
            {
                //Debug.Log($"Box Index: {boxIndex}");

                //This is inventory to empty box

                Debug.LogError($"LOOK AT THIS ONE {dice.gameObject.name} vs {holder.GetHeld().gameObject.name} 2 ");

                RollDice.Instance.diceTextures[boxIndex] = dice.visualFC.Dice;
                RollDice.Instance.AllSlots[boxIndex] = dice;//currentDice;
                //RollDice.Instance.AllDice[boxIndex] = dice.visualFC.Dice;



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
                //Debug.Log("Moving dice from inventory slot to inventory slot");

            }
            else if (oldSlot.storageType == StorageType.Hotbar && storageType == StorageType.Hotbar) //Moving dice from hotbar slot to hotbar slot
            {
                //Debug.Log]("Moving dice from hotbar slot to hotbar slot");

                RollDice.Instance.diceTextures.Swap(boxIndex, oldSlot.boxIndex);
                //RollDice.Instance.AllDice.Swap(boxIndex, oldSlot.boxIndex); 
                RollDice.Instance.AllSlots.Swap(boxIndex, oldSlot.boxIndex); 



            }
            else if (oldSlot.storageType == StorageType.Inventory) //Moved from inventory to hotbar
            {
                //Debug.Log]("Moving dice from inventory to hotbar");
                RollDice.Instance.diceTextures[boxIndex] = currentDice.visualFC.Dice;
                //RollDice.Instance.AllDice[boxIndex] = currentDice.diceTexture;
                //Debug.Log](currentDice.gameObject.name);
                RollDice.Instance.AllSlots[boxIndex] = currentDice;



                inventory.PlaceDice(other, true);  
                other.SetSlot(StorageType.Inventory); 
            }
            else //Moved from hotbar to inventory 
            {
                //Debug.Log]("Moving dice from hotbar to inventory");
                RollDice.Instance.diceTextures[oldSlot.boxIndex] = other.visualFC.Dice;
                //RollDice.Instance.AllDice[oldSlot.boxIndex] = other.diceTexture;
                RollDice.Instance.AllSlots[oldSlot.boxIndex] = other;



            }

            other.GetComponent<RawImage>().material = null;
            //Debug.Log($"Placing {dice.gameObject.name} in {this.gameObject.name} and {other.gameObject.name} in {oldSlot.gameObject.name}");
        }
    }
    

}
