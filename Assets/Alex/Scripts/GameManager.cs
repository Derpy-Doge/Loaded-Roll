using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Rolling = 0,
        Shop = 1,
        Select = 2,
    }
    public static GameManager Instance;

    [Tooltip("The text for the amount of interest")] [SerializeField] private TMPro.TMP_Text interestText;
    [Tooltip("The percent of interest added to the player's debt per installment")] [SerializeField] private float interest = .1f;
    [Tooltip("The percent of increase to interest per installment")] [SerializeField] private float interestIncrease = .02f;

    [Tooltip("The amount of rounds per debt installment")] [SerializeField] private int roundsPerDebt = 5;
    [Tooltip("The amount of rools per round")] [SerializeField] private int rollsPerRound = 3;

    public int rolls; //the amount of rolls the player has taken this round
    public float currentRound; //The current round number for this debt installment /

    [HideInInspector] public GameStates currentState = GameStates.Rolling;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is multiple Game Managers in this scene and it will not work properly");
        }
        interestText.text = $"INTEREST: {Mathf.RoundToInt(interest * 100f)}%";

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
            return false; 
        }

        else if (currentState != GameStates.Rolling) //Curent state isnt rolling
        {
            return false;
        }
        else if (RollDice.Instance.diceTextures.CheckNulls()) 
        {
            return false;
        }


        rolls++;
        currentState = GameStates.Select; 
        return true;
    }

    [ContextMenu("Add Interest")]
    private void UpdateInterest()
    {
        interest *= 1 + interestIncrease; 
        interestText.text = $"INTEREST: {Mathf.RoundToInt(interest * 100f)}%";
    }
}
