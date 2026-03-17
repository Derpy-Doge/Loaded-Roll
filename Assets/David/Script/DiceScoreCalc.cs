using NUnit.Compatibility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DiceScoreCalc : MonoBehaviour
{

    public float points;


    private float onePoints = 100f;
    private float twoPoints = 200f;
    private float threePoints = 300f;
    private float fourPoints = 400f;
    private float fivePoints = 500f;
    private float sixPoints = 600f;

    public List<float> diceValues = new List<float> {};

    [SerializeField] private List<float> Oness = new List<float> {};
    [SerializeField] private List<float> Twoss = new List<float> {};
    [SerializeField] private List<float> Threess = new List<float> {};
    [SerializeField] private List<float> Fourss = new List<float> {};
    [SerializeField] private List<float> Fivess = new List<float> {};
    [SerializeField] private List<float> Sixess = new List<float> {};

    private List<List<float>> numlist = new List<List<float>>();

    public enum ScoreCategory
    {
        None = 0,
        Standard = 1,
        ThreeOfAKind = 2,
        FourOfAKind = 3,
        FullHouse = 4,
        SmallStraight = 5,
        LargeStraight = 6,
        Yippee = 7,
        MegaYippee = 8
    }

    public ScoreCategory category;

    private void Start()
    {
        SortDice();
            numlist.Add(Oness);
            numlist.Add(Twoss);
            numlist.Add(Threess);
            numlist.Add(Fourss);
            numlist.Add(Fivess);
            numlist.Add(Sixess);
    }

    public void SortDice()
    {
        for (int i = 0; i < diceValues.Count; i++)
        {

            if (diceValues[i] == 1)
            {
                Oness.Add(1);
                Debug.Log(Oness);
            }

            else if (diceValues[i] == 2)
            {
                Twoss.Add(2);
                Debug.Log(Twoss);
            }
            else if (diceValues[i] == 3)
            {
                Threess.Add(3);
                Debug.Log(Threess);
            }
            else if (diceValues[i] == 4)
            {
                Fourss.Add(4);
                Debug.Log(Fourss);
            }
            else if (diceValues[i] == 5)
            {
                Fivess.Add(5);
                Debug.Log(Fivess);
            }
            else if (diceValues[i] == 6)
            {
                Sixess.Add(6);
                Debug.Log(Sixess);
            }

        }

        diceValues.Clear();
    }

    public void CalcMoney(int stateIndex)
    {
        // Validate index
        if (stateIndex < 0 || stateIndex >= System.Enum.GetValues(typeof(ScoreCategory)).Length)
        {
            Debug.LogWarning("Invalid enum index!");
            return;
        }

        category = (ScoreCategory)stateIndex;
        Debug.Log("Game state changed to: " + category);
        CalculateScore(category);
    }

    private void CalculateScore(ScoreCategory category)
    {
        switch (category)
        {
            case (ScoreCategory)0:
                break;
            case (ScoreCategory)1:
                Standard();
                break;
            case (ScoreCategory)2:
                ThreeOfAKind();
                break;
            case (ScoreCategory)3:
                FourOfAKind();
                break;
            case (ScoreCategory)4:
                FullHouse();
                break;
            case (ScoreCategory)5:
                SmallStraight();
                break;
            case (ScoreCategory)6:
                LargeStraight();
                break;
            case (ScoreCategory)7:
                Yippee();
                break;
            case (ScoreCategory)8:
                MegaYippee();
                break;
        }
    }

    private void Standard()
    {
        if (Oness == null)
        {
            points += 0;
            Debug.Log(points);
        }
        else
        {
            for (int i = 0; i < Oness.Count; i++)
            {
                points += onePoints;
                Debug.Log(points);
            }
        }
    

 
        if (Twoss == null)
        {
            points += 0;
            Debug.Log(points);
        }
        else
        {
            for (int i = 0; i < Twoss.Count; i++)
            {
                points += twoPoints;
                Debug.Log(points);
            }
        }
    


        if (Threess == null)
        {
            points += 0;
            Debug.Log(points);
        }

        else
        {
            for (int i = 0; i < Threess.Count; i++)
            {
                points += threePoints;
                Debug.Log(points);
            }
        }
    


        if (Fourss == null)
        {
            points += 0;
            Debug.Log(points);
        }

        else
        {
            for (int i = 0; i < Fourss.Count; i++)
            {
                points += fourPoints;
                Debug.Log(points);
            }
        }
    


        if (Fivess == null)
        {
            points += 0;
            Debug.Log(points);
        }
        else
        {
            for (int i = 0; i < Fivess.Count; i++)
            {
                points += fivePoints;
                Debug.Log(points);
            }
        }
    

   
        if (Sixess == null)
        {
            points += 0;
            Debug.Log(points);
        }

        else
        {
            for (int i = 0; i < Sixess.Count; i++)
            {
                points += sixPoints;
                Debug.Log(points);
            }
        }
    }

    private void ThreeOfAKind()
    {
        if (Oness.Count == 3)
        {
            points += (onePoints * 3) * 5;
        }

        else if (Twoss.Count == 3)
        {
            points += (twoPoints * 3) * 5;
        }

        else if (Threess.Count == 3)
        {
            points += (threePoints * 3) * 5;
        }

        else if (Fourss.Count == 3)
        {
            points += (fourPoints * 3) * 5;
        }

        else if (Fivess.Count == 3)
        {
            points += (fivePoints = 3) * 5;
        }

        else if (Sixess.Count == 3)
        {
            points += (sixPoints * 3) * 5;
        }
    }

    private void FourOfAKind()
    {
        if (Oness.Count == 4)
        {
            points += (onePoints * 4) * 5;
        }

        else if (Twoss.Count == 4)
        { 
            points += (twoPoints * 4) * 5;
        }

        else if (Threess.Count == 4)
        {
            points += (threePoints * 4) * 5;
        }

        else if (Fourss.Count == 4)
        {
            points += (fourPoints * 4) * 5;
        }

        else if (Fivess.Count == 4)
        {
            points += (fivePoints = 4) * 5;
        }

        else if (Sixess.Count == 4)
        {
            points += (sixPoints * 4) * 5;
        }
    }

    private void FullHouse()
    {
        for (int i = 0; i < numlist.Count; i++)
        {
            if (numlist[i].Count < 3)
            {
                continue;
            }


            for (int j = 0; j < numlist.Count; j++)
            {

                if (numlist[j].Count >= 2 && i != j)
                {
                    points += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;

                }
            }

                          
                       
            
        }
    }

    private void SmallStraight()
    {
        if (Oness.Count >= 1 && Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1)
        {
            points += (onePoints + twoPoints + threePoints + fourPoints) * 7;
        }

        else if ((Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1))
        {
            points += (twoPoints + threePoints + fourPoints + fivePoints) * 7;
        }

        else if ((Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1))
        { 
            points += (threePoints + fourPoints + fivePoints + sixPoints) * 7;
        }
    }

    private void LargeStraight()
    {
        if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1)
        { 
            points += (onePoints + twoPoints + threePoints + fourPoints + fivePoints) * 8;
        }

        else if (Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
        {
            points += (twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
        }
    }

    private void Yippee()
    {
        if (Oness.Count == 5)
        {
            points += (onePoints * 5) * 10;
        }

        else if (Twoss.Count == 5)
        {
            points += (twoPoints * 5) * 10;
        }

        else if (Threess.Count == 5)
        {
            points += (threePoints * 5) * 10;
        }

        else if (Fourss.Count == 5)
        {
            points += (fourPoints * 5) * 10;
        }

        else if (Fivess.Count == 5)
        {
            points += (fivePoints = 5) * 10;
        }

        else if (Sixess.Count == 5)
        {
            points += (sixPoints * 5) * 10;
        }
    }

    private void MegaYippee()
    {
        if (Oness.Count == 6)
        {
            points += (onePoints * 6) * 10;
        }

        else if (Twoss.Count == 6)
        {
            points += (twoPoints * 6) * 10;
        }

        else if (Threess.Count == 6)
        {
            points += (threePoints * 6) * 10;
        }

        else if (Fourss.Count == 6)
        {
            points += (fourPoints * 6) * 10;
        }

        else if (Fivess.Count == 6)
        {
            points += (fivePoints = 6) * 10;
        }

        else if (Sixess.Count == 6)
        {
            points += (sixPoints * 6) * 10;
        }
    }



}
