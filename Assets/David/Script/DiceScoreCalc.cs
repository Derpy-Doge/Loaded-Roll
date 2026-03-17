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

    [SerializeField] private List<float> diceValues = new List<float> {};
    [SerializeField] private List<float> Oness = new List<float> {};
    [SerializeField] private List<float> Twoss = new List<float> {};
    [SerializeField] private List<float> Threess = new List<float> {};
    [SerializeField] private List<float> Fourss = new List<float> {};
    [SerializeField] private List<float> Fivess = new List<float> {};
    [SerializeField] private List<float> Sixess = new List<float> {};

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
        Yippee = 12,
        MegaYippee = 13
    }

    public ScoreCategory category;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        CalculateScore(category);
        diceValues.Clear();
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
                Aces();
                break;
            case (ScoreCategory)2:
                Twos();
                break;
            case (ScoreCategory)3:
                Threes();
                break;
            case (ScoreCategory)4:
                Fours();
                break;
            case (ScoreCategory)5:
                Fives();
                break;
            case (ScoreCategory)6:
                Sixes();
                break;
            case (ScoreCategory)7:
                ThreeOfAKind();
                break;
            case (ScoreCategory)8:
                FourOfAKind();
                break;
            case (ScoreCategory)9:
                FullHouse();
                break;
            case (ScoreCategory)10:
                SmallStraight();
                break;
            case (ScoreCategory)11:
                LargeStraight();
                break;
            case (ScoreCategory)12:
                Yippee();
                break;
            case (ScoreCategory)13:
                MegaYippee();
                break;
        }
    }

    public void Aces()
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
    }

    public void Twos()
    {
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
    }

    public void Threes()
    {
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
    }

    public void Fours()
    {
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
    }

    public void Fives()
    {
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
    }

    public void Sixes()
    {
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

    public void ThreeOfAKind()
    {
        if (Oness.Count == 3 || Twoss.Count == 3 || Threess.Count == 3 || Fourss.Count == 3 || Fivess.Count == 3 || Sixess.Count == 3)
        {
            points += 1500;
        }
    }

    public void FourOfAKind()
    {
        if (Oness.Count == 4 || Twoss.Count == 4 || Threess.Count == 4 || Fourss.Count == 4 || Fivess.Count == 4 || Sixess.Count == 4)
        {
            points += 2000;
        }
    }

    public void FullHouse()
    {

    }

    public void SmallStraight()
    {
        if (Oness.Count >= 1 && Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 || Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 || Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1)
        {
            points += 3000;
        }
    }

    public void LargeStraight()
    {
        if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 || Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
        { 
            points += 4000;
        }
    }

    public void Yippee()
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

    public void MegaYippee()
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
