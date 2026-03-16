using UnityEngine;
using UnityEngine.InputSystem;


public class DiceHolder : MonoBehaviour
{

    [SerializeField] private GameObject[] diceBoxes;
    private DiceVisual[] visuals = new DiceVisual[5];
    public int hovered = -1;
    public static DiceHolder Instance;
    [HideInInspector] public int holding = -1;


    void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        for (int i = 0; i < diceBoxes.Length; i++)
        {
            visuals[i] = diceBoxes[i].GetComponent<DiceVisual>();
            visuals[i].boxIndex = i;
        }
    }

    public void CheckAvailability(int oldI, int newI)
    {
        visuals[oldI].boxIndex = newI;
        visuals[newI].boxIndex = oldI;
        if (!visuals[newI].containsDice) //Can move the object freely
        {
            visuals[oldI].containsDice = false;
            visuals[newI].containsDice = true;
            visuals[oldI].transform.GetChild(0).parent = visuals[newI].transform;
            

            visuals[newI] = visuals[oldI];
            visuals[oldI] = null;
            
            diceBoxes[newI] = diceBoxes[oldI];
            diceBoxes[oldI] = null;
            visuals[newI].diceImage.anchoredPosition = Vector3.zero;
        }
        else //Swap the two positions
        {

            visuals[newI].transform.GetChild(0).parent = visuals[oldI].transform;
            visuals[oldI].transform.GetChild(0).parent = visuals[newI].transform;

            var temp3 = visuals[newI].diceImage;
            visuals[newI].diceImage = visuals[oldI].diceImage; 
            visuals[oldI].diceImage = temp3; 


            var temp1 = visuals[newI];
            visuals[newI] = visuals[oldI];
            visuals[oldI] = temp1;

            var temp2 = diceBoxes[newI];
            diceBoxes[newI] = diceBoxes[oldI];
            diceBoxes[oldI] = temp2;

            visuals[newI].diceImage.anchoredPosition = Vector3.zero;
            visuals[oldI].diceImage.anchoredPosition = Vector3.zero;
            
            
        }
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (holding != -1)
            {
                visuals[holding].Click();
                
            }
            else if ( hovered != -1)
            {
                visuals[hovered].Click();
            }
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
