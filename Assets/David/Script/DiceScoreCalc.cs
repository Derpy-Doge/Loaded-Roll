using NUnit.Compatibility;
using UnityEngine;
using UnityEngine.Events;

public class DiceScoreCalc : MonoBehaviour
{
    public UnityEvent diceRoll;

   public enum ScoreCategory
    {
        Aces = 1,
        Twos = 2,
        Threes = 3,
        Fours = 4,
        Fives = 5,
        Sixes = 6,
        ThreeOfAKind = 7,
        FourOfAKind = 8,
        FullHouse = 9,
        SmallStraight = 10,
        LargeStraight = 11,
        Yippee = 12
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateScore(ScoreCategory category)
    {
        switch (category)
        {
            case (ScoreCategory)1:
                //Aces();
                break;
            case (ScoreCategory)2:
                //Twos();
                break;
            case (ScoreCategory)3:
                //Threes();
                break;
            case (ScoreCategory)4:
                //Fours();
                break;
            case (ScoreCategory)5:
                //Fives();
                break;
            case (ScoreCategory)6:
                //Sixes();
                break;
            case (ScoreCategory)7:
                //ThreeOfAKind();
                break;
            case (ScoreCategory)8:
                //FourOfAKind();
                break;
            case (ScoreCategory)9:
                //FullHouse();
                break;
            case (ScoreCategory)10:
                //SmallStraight();
                break;
            case (ScoreCategory)11:
                //LargeStraight();
                break;
            case (ScoreCategory)12:
                //Yippee();
                break;
        }
    }


 
}
