using NUnit.Compatibility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Net;

public class DiceScoreCalc : MonoBehaviour
{


    public float points;
    [HideInInspector]public float addedPoints;
    [HideInInspector]public float showpoint = 0f;

    public ShowPoints show;

    public float zeros;


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

    private void Awake()
    {
            numlist.Add(Oness);
            numlist.Add(Twoss);
            numlist.Add(Threess);
            numlist.Add(Fourss);
            numlist.Add(Fivess);
            numlist.Add(Sixess);
    }

    private void Update()
    {
        SortDice();
    }

    private void SortDice()
    {
        // sorts dice functions by checking the value of each die and adding it to the appropriate list, then clears the list of dice values for the next roll
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

    public void Button()
    {
        StartCoroutine(CalculateScore());
    }

    public IEnumerator CalculateScore()
    {
        yield return StartCoroutine(Standard());
        yield return StartCoroutine(ThreeOfAKind());
        yield return StartCoroutine(FourOfAKind());
        yield return StartCoroutine(FullHouse());
        yield return StartCoroutine(SmallStraight());
        yield return StartCoroutine(LargeStraight());
        yield return StartCoroutine(ExtraLargeStraight());
        yield return StartCoroutine(Yippee());
        yield return StartCoroutine(MegaYippee());
        yield return StartCoroutine(TotalPoints());
        yield return StartCoroutine(TotalZeros());
        yield break;
    }
    //This IEnumerator is used at the end and shows total points for this round
    IEnumerator TotalPoints()
    {
        if (!show.textFinished)
        {
            show.CalcTexts.text = "Total";
            show.textFinished = true;
            StartCoroutine(show.TotalCalc());
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            Debug.Log("total points should be leaving");
        }
    }

    //This IEnumerator is used to calculate the 0s for each point type
    private IEnumerator TotalZeros()
    {
        if (!show.textFinished)
        {
            show.CalcTexts.text = "Total Zeros";
            show.textFinished = true;
            StartCoroutine(show.TotalZeroCalc());
            yield return new WaitForSeconds(show.speed + show.CurrentTime); 

        }
    }

    //This IEnumerator is used to calculation the Standard dice rolls
    IEnumerator Standard()
    {
        Debug.Log("Standard Starts");
        if (Oness.Count == 0)
        {
            points += 0;
            Debug.Log(points);
            zeros += 1;

        }
        else if (!show.textFinished)
        {
            show.CalcTexts.text = "Standard\n1";
            Debug.Log("calculating Ones");
            for (int i = 0; i < Oness.Count; i++)
            {
                showpoint += onePoints;
                Debug.Log(showpoint);
                show.textFinished = true;
                
            }
            StartCoroutine(show.TextCalc(showpoint));
            Debug.Log($"Delay: {show.speed}  {show.CurrentTime}");
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
        }
        Debug.Log("ones finished");

        if (Twoss.Count == 0)
        {
            zeros += 1;
            Debug.Log("2?");
        }
        else if (!show.textFinished)
        {
            show.CalcTexts.text = "Standard\n2";
            Debug.Log("calculating twos");
            for (int i = 0; i < Twoss.Count; i++)
            {
                Debug.Log("adding twos to points");
                showpoint += twoPoints;
                Debug.Log(points);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
        }


        if (Threess.Count == 0)
        {
            points += 0;
            Debug.Log(points);
            zeros += 1;

        }

        else if (!show.textFinished)
        {
            show.CalcTexts.text = "Standard\n3";
            Debug.Log("calculating Threes");
            for (int i = 0; i < Threess.Count; i++)
            {
                showpoint += threePoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
        }


        if (Fourss.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
            zeros += 1;

        }

        else if (!show.textFinished)
        {
            show.CalcTexts.text = "Standard\n4";
            Debug.Log("calculating Fours");
            for (int i = 0; i < Fourss.Count; i++)
            {
                showpoint += fourPoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
        }


        if (Fivess.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
            zeros += 1;

        }
        else if (!show.textFinished)
        {
            show.CalcTexts.text = "Standard\n5";
            Debug.Log("calculating Fives");
            for (int i = 0; i < Fivess.Count; i++)
            {
                showpoint += fivePoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
        }


        if (Sixess.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
            zeros += 1;

        }

        else if (!show.textFinished)
        {
            show.CalcTexts.text = "Standard\n6";
            Debug.Log("calculating Sixes");
            for (int i = 0; i < Sixess.Count; i++)
            {
                showpoint += sixPoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
        }
    }

    private IEnumerator ThreeOfAKind()
    {
        Debug.Log("kind of three is started?");

        if (!show.textFinished)
        {
            float temp = addedPoints;
            Debug.Log("Three of a kind starts");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 3)
                {
                    show.CalcTexts.text = "Three Of A Kind\n" + numlist[i][0];
                    showpoint += ((numlist[i][0] * 100) * 3) * 4;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }

            }

            if (addedPoints == temp)
            {
                zeros += 1;
                Debug.Log("zeros" + zeros);
            }

        }
    }

    private IEnumerator FourOfAKind()
    {
        
        if (!show.textFinished)
        {
            float temp = addedPoints;
            show.CalcTexts.text = "Four Of A Kind";
            Debug.Log("Four of a kind starts");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 4)
                {
                    Debug.Log(numlist[i]);
                    showpoint += ((numlist[i][0] * 100) * 4) * 5;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }
            }


            if (addedPoints == temp)
            {
                zeros += 1;
                Debug.Log("zeros" + zeros);
            }
        }
    }

    private IEnumerator FullHouse()
    {
        
        if (!show.textFinished)
        {
            float temp = addedPoints;
            show.CalcTexts.text = "Full House";
            Debug.Log("Full house starts");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count < 3)
                {
                    Debug.Log(numlist[i] + "is less than three");
                    continue;

                }


                for (int j = 0; j < numlist.Count; j++)
                {

                    if (numlist[j].Count >= 5 && i == j)
                    {

                        showpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                        show.textFinished = true;
                        StartCoroutine(show.TextCalc(showpoint));
                        yield return new WaitForSeconds(show.speed + show.CurrentTime);
                    }

                    if (numlist[j].Count >= 2 && i != j)
                    {
                        showpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                        show.textFinished = true;
                        StartCoroutine(show.TextCalc(showpoint));
                        yield return new WaitForSeconds(show.speed + show.CurrentTime);
                    }
                }
            }


            if (addedPoints == temp)
            {
                zeros += 1;
            }
        }
    }

    private IEnumerator SmallStraight()
    {
       
        if (!show.textFinished)
        {
            float temp1 = addedPoints;
            float temp2 = addedPoints;
            float temp3 = addedPoints;
            Debug.Log("is smal straight running");
            Debug.Log("Small straight starts");
            if (Oness.Count >= 1 && Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1)
            {
                show.CalcTexts.text = "Small Straight\n1,2,3,4";
                showpoint += (onePoints + twoPoints + threePoints + fourPoints) * 7;
                Debug.Log(showpoint);
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

            else if (addedPoints == temp1)
            {
                zeros += 1;
            }

            if ((Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1))
            {
                show.CalcTexts.text = "Small Straight\n2,3,4,5";
                showpoint += (twoPoints + threePoints + fourPoints + fivePoints) * 7;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

            else if (addedPoints == temp2)
            {
                zeros += 1;
            }

            if ((Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1))
            {
                show.CalcTexts.text = "Small Straight\n3,4,5,6";
                showpoint += (threePoints + fourPoints + fivePoints + sixPoints) * 7;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

            else if (addedPoints == temp3)
             {
                 zeros += 1;
            }
        }
    }

    private IEnumerator LargeStraight()
    {
        
        if (!show.textFinished)
        {
            float temp1 = addedPoints;
            float temp2 = addedPoints;
            Debug.Log("Large straight");
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1)
            {
                show.CalcTexts.text = "Large Straight\n1,2,3,4,5";
                showpoint += (onePoints + twoPoints + threePoints + fourPoints + fivePoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);


            }
            else if (addedPoints == temp1)
            {
                zeros += 1;
            }

            if (Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                show.CalcTexts.text = "Large Straight\n2,3,4,5,6";
                showpoint += (twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

               else if (addedPoints == temp2)
                {
                    zeros += 1;
            }



        }
        
    }

    private IEnumerator ExtraLargeStraight()
    {

        if (!show.textFinished)
        {
            float temp = addedPoints;
            show.CalcTexts.text = "Extra Large Straight";
            Debug.Log("Extra Large straight");
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                showpoint += (onePoints + twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

            else if (addedPoints == temp)
            {
                zeros += 1;
            }
        }
        
    }

    private IEnumerator Yippee()
    {
        
        if (!show.textFinished)
        {
            float temp = addedPoints;
            show.CalcTexts.text = "YIPPEE!!!";
            Debug.Log("yippee");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 5)
                {
                    showpoint += ((numlist[i][0] * 100) * 5) * 10;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }
            }


            if (addedPoints == temp)
            {
                zeros += 1;
            }
        }
    }

    private IEnumerator MegaYippee()
    {
        
        if (!show.textFinished)
        {
            float temp = addedPoints;
            show.CalcTexts.text = "MEGA YIPPEE!!!!!!";
            Debug.Log("Mega yippee");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count == 6)
                {
                    showpoint += ((numlist[i][0] * 100) * 6) * 10;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }
            }

            if (addedPoints == temp)
            {
                zeros += 1;
            }
        }

    }



}
