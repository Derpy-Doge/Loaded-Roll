using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Rolling = 0,
        Shop = 1,
        Select = 2,
        Busy = 3,
    }

    public enum AnimDir
    {
        Out = 0,
        In = 1,
    }
    public static GameManager Instance;

    [Tooltip("The text for the amount of interest")] [SerializeField] private TMPro.TMP_Text interestText;
    [Tooltip("The percent of interest added to the player's debt per installment")] [SerializeField] private float interest = .1f;
    [Tooltip("The percent of increase to interest per installment")] [SerializeField] private float interestIncrease = .02f;

    [Tooltip("The amount of rounds per debt installment")] [SerializeField] private int roundsPerDebt = 5;
    [Tooltip("The amount of rools per round")] public int rollsPerRound = 3;
    [Tooltip("The Animator Component for the inventory.")] [SerializeField] private Animator invAnim; 
    [Tooltip("The Animator Component for the back recycling bin.")] [SerializeField] private Animator recycleAnimOne; 
    [Tooltip("The Animator Component for the front recycling bin.")] [SerializeField] private Animator recycleAnimTwo; 

    [Tooltip("The Animator Component for the roll / select button.")] [SerializeField] private Animator rollAnim; 

    [Tooltip("The Sprite for the roll button.")] [SerializeField] private Sprite rollSprite;
    [Tooltip("The Sprite for the select button.")] [SerializeField] private Sprite selectSprite; 
    [Tooltip("The Text for the roll/select button.")] [SerializeField] private TMPro.TMP_Text rollText; 

    [Tooltip("The Button for roll & select.")] [SerializeField] private Button stateButton;
    [Tooltip("The dice manager.")][SerializeField] private GameObject DiceManager;

    public TMPro.TMP_Text tutorialTitle; 
    public TMPro.TMP_Text tutorialDescription; 
    public GameObject tutorialMessage; //The GO with the tutorialStuff 


    [Range(1f, 10f)] [SerializeField] private float glowAmount;

    public int rolls; //the amount of rolls the player has taken this round
    public float currentRound; //The current round number for this debt installment /



    [HideInInspector] public float recycleCDOne = 1.1f;
    [HideInInspector] public float recycleCDTwo = .75f;

    [HideInInspector] public GameStates CurrentState = GameStates.Rolling;

    void Awake()
    {
        //Points = SaveDataController.Instance.current.run.points;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is multiple Game Managers in this scene and it will not work properly");
        }
        interestText.text = $"INTEREST: {Mathf.RoundToInt(interest * 100f)}%";
        interestText.fontMaterial.SetVector("_FaceColor", new Vector4(glowAmount, 0.5f, 0.5f, 1.0f)); 

    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public bool Roll()
    {
        if (rolls + 1 > rollsPerRound) //New Round 
        {
            Debug.Log("TRUE");
            return false; 
        }

        else if (CurrentState != GameStates.Rolling) //Curent state isnt rolling
        {
            return false;
        }
        //else if (RollDice.Instance.diceTextures.CheckNulls()) //check this later
        //{
        //    return false;
        //}

        invAnim.SetTrigger("Out");
        StartCoroutine(AnimateWithCooldown(recycleAnimOne, "In", .25f));
        StartCoroutine(AnimateWithCooldown(recycleAnimTwo, "In", .25f));
        rollAnim.SetTrigger("Shrink");

        rolls++;
        CurrentState = GameStates.Busy; 
        return true;
    }

    public IEnumerator RefillInventory()
    {
        yield return new WaitForSeconds(recycleCDOne);
        recycleAnimOne.SetTrigger("Empty");
        recycleAnimTwo.SetTrigger("Empty");
        yield return new WaitForSeconds(recycleCDTwo);
        CurrentState = GameStates.Rolling;
        // StartCoroutine(AnimateWithCooldown(recycleAnimOne, "Empty", 1.1f));
        // StartCoroutine(AnimateWithCooldown(recycleAnimTwo, "Empty", 1.1f));
    }

    public void SwapInventory(int dir)
    {
        if (dir == (int)AnimDir.In)
        {
            invAnim.SetTrigger("Out");
            StartCoroutine(AnimateWithCooldown(recycleAnimOne, "In", .25f));
            StartCoroutine(AnimateWithCooldown(recycleAnimTwo, "In", .25f));
        }
        else
        {
            recycleAnimOne.SetTrigger("Out");
            recycleAnimTwo.SetTrigger("Out");
            StartCoroutine(AnimateWithCooldown(invAnim, "In", .25f));
        }
    }

    public void SwapInventory(AnimDir dir)
    {
        if (dir == AnimDir.In)
        {
            invAnim.SetTrigger("Out");
            StartCoroutine(AnimateWithCooldown(recycleAnimOne, "In", .25f));
            StartCoroutine(AnimateWithCooldown(recycleAnimTwo, "In", .25f));
        }
        else
        {
            recycleAnimOne.SetTrigger("Out");
            recycleAnimTwo.SetTrigger("Out");
            StartCoroutine(AnimateWithCooldown(invAnim, "In", .25f));
        }
    }

    IEnumerator AnimateWithCooldown(Animator anim, string triggerType, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        anim.SetTrigger(triggerType);
    }

    public void AnimateEmpty()
    {
        recycleAnimOne.SetTrigger("Empty");
        recycleAnimTwo.SetTrigger("Empty");

    }

    public void SwapStateButton()
    {   
        Image stateImage = stateButton.gameObject.GetComponent<Image>();
        rollAnim.SetTrigger("Grow");

        if (stateImage.sprite == rollSprite)
        {
            stateImage.sprite = selectSprite;
            rollText.text = "Select";
        }
        else
        {
            stateImage.sprite = rollSprite;
            rollText.text = "Roll";
            
        }
    }

    [ContextMenu("hhhoojafaf")]

    public void NewDebtInstallment()
    {
        DiceManager.SendMessage("ResetValues");
        //SaveDataController.Instance.current.run.Points += 10;
        //SendMessage("Save");
        //SaveDataController.Instance.Save();
    }


    [ContextMenu("Add Interest")]
    private void UpdateInterest()
    {
        interest *= 1 + interestIncrease; 
        interestText.text = $"INTEREST: {Mathf.RoundToInt(interest * 100f)}%";
    }


}
