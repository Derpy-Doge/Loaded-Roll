using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DiceScoreCalc : MonoBehaviour
{


    public float points;
    public float addedPoints;
    public float showpoint = 0f;

    public ShowPoints show;

    public float zeros;

    private bool isrunning;


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

    public void RollButton()
    {
        SortDice();
        CalculateTotal();
        StartCoroutine(CalculateScore());
    }

    public void Skip()
    { 
        if (isrunning)
        {
            StartCoroutine(TotalPoints());
        }
        
        //StartCoroutine(TotalZeros());
    }

    public void OIJaojgojaogja(List<float> faces)
    {
        diceValues = faces;
        SortDice();
        CalculateTotal();
        isrunning = true;
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
            show.textFinished = true;
            StartCoroutine(show.TotalCalc("Total"));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            Debug.Log("total points should be leaving");
            isrunning = false;
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
            Oness.Clear();
            Twoss.Clear();
            Threess.Clear();
            Fourss.Clear();
            Fivess.Clear();
            Sixess.Clear();
        }
    }

    //This IEnumerator is used to calculation the Standard dice rolls
    private IEnumerator Standard()
    {
        Debug.Log("Standard Starts");
        if (Oness.Count == 0)
        {
            points += 0;
            Debug.Log(points);

        }
        else if (!show.textFinished)
        {
            Debug.Log("calculating Ones");
            for (int i = 0; i < Oness.Count; i++)
            {
                showpoint += onePoints;
                Debug.Log(showpoint);
                show.textFinished = true;
                
            }
            StartCoroutine(show.TextCalc(showpoint,"Standard\n1"));
            Debug.Log($"Delay: {show.speed}  {show.CurrentTime}");
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            showpoint = 0f;
        }
        Debug.Log("ones finished");

        if (Twoss.Count == 0)
        {
            Debug.Log("2?");
        }
        else if (!show.textFinished)
        {
            Debug.Log("calculating twos");
            for (int i = 0; i < Twoss.Count; i++)
            {
                Debug.Log("adding twos to points");
                showpoint += twoPoints;
                Debug.Log(points);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint,"Standard\n2"));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            showpoint = 0f;

        }


        if (Threess.Count == 0)
        {
            points += 0;
            Debug.Log(points);

        }

        else if (!show.textFinished)
        {
            Debug.Log("calculating Threes");
            for (int i = 0; i < Threess.Count; i++)
            {
                showpoint += threePoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint, "Standard\n3"));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            showpoint = 0f;

        }


        if (Fourss.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);

        }

        else if (!show.textFinished)
        {
            Debug.Log("calculating Fours");
            for (int i = 0; i < Fourss.Count; i++)
            {
                showpoint += fourPoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint, "Standard\n4"));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            showpoint = 0f;

        }


        if (Fivess.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);

        }
        else if (!show.textFinished)
        {
            Debug.Log("calculating Fives");
            for (int i = 0; i < Fivess.Count; i++)
            {
                showpoint += fivePoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint, "Standard\n5"));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            showpoint = 0f;

        }


        if (Sixess.Count == 0)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);

        }

        else if (!show.textFinished)
        {
            Debug.Log("calculating Sixes");
            for (int i = 0; i < Sixess.Count; i++)
            {
                showpoint += sixPoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(showpoint, "Standard\n6"));
            yield return new WaitForSeconds(show.speed + show.CurrentTime);
            showpoint = 0f;

        }
    }

    private IEnumerator ThreeOfAKind()
    {
        Debug.Log("kind of three is started?");

        if (!show.textFinished)
        {
            Debug.Log("Three of a kind starts");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 3)
                {
                    showpoint += ((numlist[i][0] * 100) * 3) * 4;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint, "Three Of A Kind\n" + numlist[i][0]));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }

            }

        }
    }

    private IEnumerator FourOfAKind()
    {
        
        if (!show.textFinished)
        {
            Debug.Log("Four of a kind starts");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 4)
                {
                    Debug.Log(numlist[i]);
                    showpoint += ((numlist[i][0] * 100) * 4) * 5;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint, "Four Of A Kind\n" + numlist[i][0]));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }
            }

        }
    }

    private IEnumerator FullHouse()
    {
        
        if (!show.textFinished)
        {
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
                        StartCoroutine(show.TextCalc(showpoint, "Full House\n" + numlist[i][0] + " & " + numlist[j][0]));
                        yield return new WaitForSeconds(show.speed + show.CurrentTime);
                    }

                    if (numlist[j].Count >= 2 && i != j)
                    {
                        showpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                        show.textFinished = true;
                        StartCoroutine(show.TextCalc(showpoint, "Full House\n" + numlist[i][0] + " & " + numlist[j][0]));
                        yield return new WaitForSeconds(show.speed + show.CurrentTime);
                    }
                }
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
                showpoint += (onePoints + twoPoints + threePoints + fourPoints) * 7;
                Debug.Log(showpoint);
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint, "Small Straight\n1,2,3,4"));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

            if ((Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1))
            {
                showpoint += (twoPoints + threePoints + fourPoints + fivePoints) * 7;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint, "Small Straight\n2,3,4,5"));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

            if ((Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1))
            {
                showpoint += (threePoints + fourPoints + fivePoints + sixPoints) * 7;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint, "Small Straight\n3,4,5,6"));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

        }
    }

    private IEnumerator LargeStraight()
    {
        
        if (!show.textFinished)
        {
            Debug.Log("Large straight");
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1)
            {
                showpoint += (onePoints + twoPoints + threePoints + fourPoints + fivePoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint, "Large Straight\n1,2,3,4,5"));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);


            }

            if (Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                showpoint += (twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint, "Large Straight\n2,3,4,5,6"));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }

        }
        
    }

    private IEnumerator ExtraLargeStraight()
    {

        if (!show.textFinished)
        {
            Debug.Log("Extra Large straight");
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                showpoint += (onePoints + twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
                show.textFinished = true;
                StartCoroutine(show.TextCalc(showpoint, "Extra Large Straight"));
                yield return new WaitForSeconds(show.speed + show.CurrentTime);
            }
        }
        
    }

    private IEnumerator Yippee()
    {
        
        if (!show.textFinished)
        {
            Debug.Log("yippee");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 5)
                {
                    showpoint += ((numlist[i][0] * 100) * 5) * 10;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint, "YIPPEE!!!"));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }
            }
        }
    }

    private IEnumerator MegaYippee()
    {
        
        if (!show.textFinished)
        {
            Debug.Log("Mega yippee");
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count == 6)
                {
                    showpoint += ((numlist[i][0] * 100) * 6) * 10;
                    show.textFinished = true;
                    StartCoroutine(show.TextCalc(showpoint, "MEGA YIPPEE!!!!!!"));
                    yield return new WaitForSeconds(show.speed + show.CurrentTime);
                }
            }
        }

    }


    public void CalculateTotal()
    {


            if (Oness.Count == 0)
            {
                zeros += 1;
            }
            else
            {
                for (int i = 0; i < Oness.Count; i++)
                {
                    addedPoints += onePoints;

                }
            }


            if (Twoss.Count == 0)
            {
                zeros += 1;
            }
            else
            {
                for (int i = 0; i < Twoss.Count; i++)
                {
                    addedPoints += twoPoints;
                }
            }


            if (Threess.Count == 0)
            {
                zeros += 1;

            }

            else
            {
                for (int i = 0; i < Threess.Count; i++)
                {
                    addedPoints += threePoints;
                }
            }


            if (Fourss.Count == 0)
            {
                zeros += 1;
            }

            else
            {
                for (int i = 0; i < Fourss.Count; i++)
                {
                    addedPoints += fourPoints;
                }
            }


            if (Fivess.Count == 0)
            {
                zeros += 1;
            }
            else 
            {
                for (int i = 0; i < Fivess.Count; i++)
                {
                    addedPoints += fivePoints;
                }
            }


            if (Sixess.Count == 0)
            {
                zeros += 1;
            }

            else
            {
                for (int i = 0; i < Sixess.Count; i++)
                {
                    addedPoints += sixPoints;
                }
            }
        


            float tempThree = addedPoints;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 3)
                {
                    addedPoints += ((numlist[i][0] * 100) * 3) * 4;
                }

            }

            if (addedPoints == tempThree)
            {
                zeros += 1;
            }

        

            float tempFour = addedPoints;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 4)
                {
                    addedPoints += ((numlist[i][0] * 100) * 4) * 5;
                }
            }


            if (addedPoints == tempFour)
            {
                zeros += 1;
            }

            float tempFull = addedPoints;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count < 3)
                {
                    continue;
                }


                for (int j = 0; j < numlist.Count; j++)
                {

                    if (numlist[j].Count >= 5 && i == j)
                    {

                        addedPoints += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                    }

                    if (numlist[j].Count >= 2 && i != j)
                    {
                        addedPoints += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                    }
                }
            }


            if (addedPoints == tempFull)
            {
                zeros += 1;
            }


            float tempSmall1 = addedPoints;
            float tempSmall2 = addedPoints;
            float tempSmall3 = addedPoints;
            if (Oness.Count >= 1 && Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1)
            {
                addedPoints += (onePoints + twoPoints + threePoints + fourPoints) * 7;
            }

            else if (addedPoints == tempSmall1)
            {
                zeros += 1;
            }

            if ((Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1))
            {
                addedPoints += (twoPoints + threePoints + fourPoints + fivePoints) * 7;
            }

            else if (addedPoints == tempSmall2)
            {
                zeros += 1;
            }

            if ((Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1))
            {
                addedPoints += (threePoints + fourPoints + fivePoints + sixPoints) * 7;

            }

            else if (addedPoints == tempSmall3)
            {
                zeros += 1;
            }

            float tempBig1 = addedPoints;
            float tempBig2 = addedPoints;
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1)
            {
                addedPoints += (onePoints + twoPoints + threePoints + fourPoints + fivePoints) * 8;
            }
            else if (addedPoints == tempBig1)
            {
                zeros += 1;
            }

            if (Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                addedPoints += (twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
            }

            else if (addedPoints == tempBig2)
            {
                zeros += 1;
            }


            float tempHuge = addedPoints;
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                addedPoints += (onePoints + twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
            }

            else if (addedPoints == tempHuge)
            {
                zeros += 1;
            }

            float tempYippee = addedPoints;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 5)
                {
                    addedPoints += ((numlist[i][0] * 100) * 5) * 10;
                }
            }


            if (addedPoints == tempYippee)
            {
                zeros += 1;
            }

            float tempMegaYippee = addedPoints;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count == 6)
                {
                    addedPoints += ((numlist[i][0] * 100) * 6) * 10;
                }
            }

            if (addedPoints == tempMegaYippee)
            {
                zeros += 1;
            }
        }

    }


