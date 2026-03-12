using UnityEngine;
using UnityEngine.Events;

public class DiceScoreCalc : MonoBehaviour
{
    public

    enum ScoreCategory
    {
        Aces,
        Twos,
        Threes,
        Fours,
        Fives,
        Sixes,
        ThreeOfAKind,
        FourOfAKind,
        FullHouse,
        SmallStraight,
        LargeStraight,
        Yippee
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateScore(ScoreCategory category, int[] diceValues)
    {
        switch (category)
        {
            case ScoreCategory.Aces:
                //Aces();
                break;
            case ScoreCategory.Twos:
                //Twos();
                break;
            case ScoreCategory.Threes:
                //Threes();
                break;
            case ScoreCategory.Fours:
                //Fours();
                break;
            case ScoreCategory.Fives:
                //Fives();
                break;
            case ScoreCategory.Sixes:
                //Sixes();
                break;
            case ScoreCategory.ThreeOfAKind:
                //ThreeOfAKind();
                break;
            case ScoreCategory.FourOfAKind:
                //FourOfAKind();
                break;
            case ScoreCategory.FullHouse:
                //FullHouse();
                break;
            case ScoreCategory.SmallStraight:
                //SmallStraight();
                break;
            case ScoreCategory.LargeStraight:
                //LargeStraight();
                break;
            case ScoreCategory.Yippee:
                //Yippee();
                break;
        }
    }


 
}
