using NUnit.Compatibility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiceScoreCalc : MonoBehaviour
{


    public float points;
    [HideInInspector]public float addedPoints;
    [HideInInspector]public float showpoint = 0f;

    public ShowPoints show;

    private float numadd = 20;


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
        show.textFinished = false;
        StartCoroutine(Standard());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(ThreeOfAKind());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(FourOfAKind());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(FullHouse());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(SmallStraight());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(LargeStraight());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(ExtraLargeStraight());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(Yippee());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(MegaYippee());
        yield return new WaitForSeconds(show.CurrentTime + numadd);
        show.textFinished = false;
        StartCoroutine(TotalPoints());
        yield break;
    }
    //This IEnumerator is used at the end and shows total points for this round
    IEnumerator TotalPoints()
    {
        show.CalcTexts.text = "Total";
        show.textFinished = true;
        StartCoroutine(show.TotalCalc());
        yield break;
    }

    //This IEnumerator is used to calculation the Standard dice rolls
     IEnumerator Standard()
    {
        Debug.Log("Standard Starts");
        
        if (Oness.Count == 0)
        {
            points += 0;
            Debug.Log(points);
            if (showpoint == 0)
            {
                show.CalcTexts.text = "Standard\n1";
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + numadd);
            }
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
            yield return new WaitForSeconds(show.CurrentTime + numadd);
            
        }
        Debug.Log("ones finished");

        if (Twoss.Count == 0)
        {
            if (!show.textFinished)
            {
                Debug.Log("2?");
                if (showpoint == 0)
                {
                    show.CalcTexts.text = "Standard\n2";
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.CurrentTime + numadd);
                }
            }
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
            yield return new WaitForSeconds(show.CurrentTime + numadd);
        }


        if (Threess.Count == 0)
        {
            points += 0;
            Debug.Log(points);
            if (showpoint == 0)
            {
                show.CalcTexts.text = "Standard\n3";
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + numadd);
            }
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
            yield return new WaitForSeconds(show.CurrentTime + numadd);

        }


        if (Fourss.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
            if (showpoint == 0)
            {
                show.CalcTexts.text = "Standard\n4";
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + numadd);
            }
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
            yield return new WaitForSeconds(show.CurrentTime + numadd);

        }


        if (Fivess.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
            if (showpoint == 0)
            {
                show.CalcTexts.text = "Standard\n5";
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + numadd);
            }
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
            yield return new WaitForSeconds(show.CurrentTime + numadd);

        }


        if (Sixess.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
            if (showpoint == 0)
            {
                show.CalcTexts.text = "Standard\n6";
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + numadd);
            }
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
            yield return new WaitForSeconds(show.CurrentTime + numadd);

        }
        yield return new WaitForSeconds(show.CurrentTime + numadd);
    }

    private IEnumerator ThreeOfAKind()
    {
        if (!show.textFinished)
        {
            
            Debug.Log("Three of a kind starts");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 3)
                {
                    show.CalcTexts.text = "Three Of A Kind\n" + numlist[i][0];
                    showpoint += ((numlist[i][0] * 100) * 3) * 4;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.CurrentTime + 10);
                }

                


            }
            if (showpoint == 0)
                {
                    Debug.Log("nothreeofakind");
                    show.CalcTexts.text = "Three Of A Kind";
                    showpoint = 0;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.CurrentTime + 10);
                }
            
        }
        yield return new WaitForSeconds(show.CurrentTime + 1);

    }

    private IEnumerator FourOfAKind()
    {
        
        if (!show.textFinished)
        {
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
                    yield return new WaitForSeconds(show.CurrentTime + 1);

                }

                else
                {
                    showpoint = 0;
                    StartCoroutine(show.TextCalc(showpoint));
                    yield return new WaitForSeconds(show.CurrentTime + 1);
                }
                yield return null;
            }
            
           
        }
    }

    private IEnumerator FullHouse()
    {
        
        if (!show.textFinished)
        {
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
                    if (numlist[j].Count < 2)
                    {
                        showpoint = 0;
                        StartCoroutine(show.TextCalc(showpoint));
                        yield return new WaitForSeconds(show.CurrentTime + 1);

                    }

                    if (numlist[j].Count >= 5 && i == j)
                    {

                        showpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                        show.textFinished = true;
                        StartCoroutine(show.TextCalc(showpoint));
                        yield return new WaitForSeconds(show.CurrentTime + 1);

                    }

                    if (numlist[j].Count >= 2 && i != j)
                    {
                        showpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                        show.textFinished = true;
                        StartCoroutine(show.TextCalc(showpoint));
                        yield return new WaitForSeconds(show.CurrentTime + 1);

                    }
                }

                yield return null;
            }
        }
    }

    private IEnumerator SmallStraight()
    {
       
        if (!show.textFinished)
        {
             Debug.Log("is smal straight running");
            Debug.Log("Small straight starts");
            if (Oness.Count >= 1 && Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1)
            {
                show.CalcTexts.text = "Small Straight\n1,2,3,4";
                showpoint += (onePoints + twoPoints + threePoints + fourPoints) * 7;
                Debug.Log(showpoint);
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }

            if ((Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1))
            {
                show.CalcTexts.text = "Small Straight\n2,3,4,5";
                showpoint += (twoPoints + threePoints + fourPoints + fivePoints) * 7;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }

            if ((Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1))
            {
                show.CalcTexts.text = "Small Straight\n3,4,5,6";
                showpoint += (threePoints + fourPoints + fivePoints + sixPoints) * 7;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }

            if (showpoint == 0)
            {
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }
        }
        yield return null;

    }

    private IEnumerator LargeStraight()
    {
        
        if (!show.textFinished)
        {
            Debug.Log("Large straight");
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1)
            {
                show.CalcTexts.text = "Large Straight\n1,2,3,4,5";
                showpoint += (onePoints + twoPoints + threePoints + fourPoints + fivePoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }

            if (Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                show.CalcTexts.text = "Large Straight\n1,2,3,4,5";
                showpoint += (twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
            }
            if (showpoint == 0)
            {
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }
        }
        yield return new WaitForSeconds(show.CurrentTime + 1);
    }

    private IEnumerator ExtraLargeStraight()
    {

        if (!show.textFinished)
        {
            show.CalcTexts.text = "Extra Large Straight";
            Debug.Log("Extra Large straight");
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                showpoint += (onePoints + twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint));
            }
            else
            { 
                showpoint = 0;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }


        }
        yield return new WaitForSeconds(show.CurrentTime + 1);
    }

    private IEnumerator Yippee()
    {
        
        if (!show.textFinished)
        {
            show.CalcTexts.text = "YIPPEE!!!";
            Debug.Log("yippee");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 5)
                {
                    showpoint += ((numlist[i][0] * 100) * 5) * 10;
                    show.textFinished = true;
                }
            }
            if (showpoint == 0)
            {
                showpoint = 0;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.CurrentTime + 1);
        }
    }

    private IEnumerator MegaYippee()
    {
        
        if (!show.textFinished)
        {
            show.CalcTexts.text = "MEGA YIPPEE!!!!!!";
            Debug.Log("Mega yippee");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count == 6)
                {
                    showpoint += ((numlist[i][0] * 100) * 6) * 10;
                    show.textFinished = true;
                }
            }

            if (showpoint == 0)
            {
                showpoint = 0;
                StartCoroutine(show.TextCalc(showpoint));
                yield return new WaitForSeconds(show.CurrentTime + 1);
            }
            StartCoroutine(show.TextCalc(showpoint));
            yield return new WaitForSeconds(show.CurrentTime + 1);
        }

    }



}
