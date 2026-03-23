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
    public float addedPoints;

    public float timer;
    private float currentTime;

    public ShowPoints show;

    public static DiceScoreCalc Instance { get; private set; }

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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
    }
    private void Start()
    {
        timer = show.CurrentTime;
        currentTime = timer;
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


    public void CalculateScore()
    { 
        StartCoroutine(Standard());
        StartCoroutine(ThreeOfAKind());
        StartCoroutine(FourOfAKind());
        StartCoroutine(FullHouse());
        SmallStraight();
        LargeStraight();
        StartCoroutine(Yippee());
        StartCoroutine(MegaYippee());
    }

    
     IEnumerator Standard()
    {
        //show.CalcTexts.text = "Standard";
        //StartCoroutine(show.CalcTextEnter());
        if (Oness == null)
        {
            points += 0;
            Debug.Log(points);
        }
        else if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < Oness.Count; i++)
            {
                addedpoint += onePoints;
                Debug.Log(addedpoint);
                show.textFinished = true;
            }
            
            StartCoroutine(show.TextCalc(addedpoint));
            yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }



        if (Twoss == null)
        {
            points += 0;
            Debug.Log(points);
        }
        else if (!show.textFinished)
        {
            Debug.Log("calculating twos");
            float addedpoint = 0f;
            for (int i = 0; i < Twoss.Count; i++)
            {
                Debug.Log("adding twos to points");
                addedpoint += twoPoints;
                Debug.Log(points);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(addedpoint));
            //yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }

       
        if (Threess == null)
        {
            points += 0;
            Debug.Log(points);
        }

        else if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < Threess.Count; i++)
            {
                addedpoint += threePoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(addedpoint));
            //yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    


        if (Fourss == null)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
        }

        else if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < Fourss.Count; i++)
            {
                addedpoint += fourPoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(addedpoint));
            //yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    


        if (Fivess == null)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
        }
        else if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < Fivess.Count; i++)
            {
                addedpoint += fivePoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(addedpoint));
            //yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    

   
        if (Sixess == null)
        {
            addedPoints += 0;
            Debug.Log(addedPoints);
        }

        else if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < Sixess.Count; i++)
            {
                addedpoint += sixPoints;
                Debug.Log(addedPoints);
                show.textFinished = true;
            }
            StartCoroutine(show.TextCalc(addedpoint));
            //yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }

    }

    private IEnumerator ThreeOfAKind()
    {
        if (!show.textFinished)
        { 
            float addedpoint = 0f;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 3)
                {
                    addedpoint += ((numlist[i][0] * 100) * 3) * 4;
                    show.textFinished = true;
                }
            }
            StartCoroutine(show.TextCalc(addedpoint));
            yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    }

    private IEnumerator FourOfAKind()
    {
        if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 4)
                {
                    Debug.Log(numlist[i]);
                    addedpoint += ((numlist[i][0] * 100) * 4) * 5;
                    show.textFinished = true;
                }
                
            }
            StartCoroutine(show.TextCalc(addedpoint));
            yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    }

    private IEnumerator FullHouse()
    {
        if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count < 3)
                {
                    continue;
                }

                
                for (int j = 0; j < numlist.Count; j++)
                {
                    
                    if (numlist[j].Count >= 2 && i == j)
                    {
                        if (j >= 5)
                        {
                            addedpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                            show.textFinished = true;
                        }
                    }

                    if (numlist[j].Count >= 2 && i != j)
                    {
                        addedpoint += (((numlist[i][0] * 100) * 3) + ((numlist[j][0] * 100) * 2)) * 6;
                        show.textFinished = true;

                    }
                }

            }
            StartCoroutine(show.TextCalc(addedpoint));
            yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    }

    private void SmallStraight()
    {
        if (!show.textFinished)
        {
            if (Oness.Count >= 1 && Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1)
            {
                addedPoints += (onePoints + twoPoints + threePoints + fourPoints) * 7;
            }

            else if ((Twoss.Count >= 1 && Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1))
            {
                addedPoints += (twoPoints + threePoints + fourPoints + fivePoints) * 7;
            }

            else if ((Threess.Count >= 1 && Fourss.Count >= 1 && Fivess.Count >= 1 && Sixess.Count >= 1))
            {
                addedPoints += (threePoints + fourPoints + fivePoints + sixPoints) * 7;
            }
        }
    }

    private void LargeStraight()
    {
        if (!show.textFinished)
        {
            if (Oness.Count == 1 && Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1)
            {
                addedPoints += (onePoints + twoPoints + threePoints + fourPoints + fivePoints) * 8;
            }

            else if (Twoss.Count == 1 && Threess.Count == 1 && Fourss.Count == 1 && Fivess.Count == 1 && Sixess.Count == 1)
            {
                addedPoints += (twoPoints + threePoints + fourPoints + fivePoints + sixPoints) * 8;
            }
        }
    }

    private IEnumerator Yippee()
    {
        if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count >= 5)
                {
                    addedpoint += ((numlist[i][0] * 100) * 5) * 10;
                    show.textFinished = true;
                }
            }
            StartCoroutine(show.TextCalc(addedpoint));
            yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }
    }

    private IEnumerator MegaYippee()
    {
        if (!show.textFinished)
        {
            float addedpoint = 0f;
            for (int i = 0; i < numlist.Count; i++)
            {
                if (numlist[i].Count == 6)
                {
                    addedpoint += ((numlist[i][0] * 100) * 6) * 10;
                }
            }
            StartCoroutine(show.TextCalc(addedpoint));
            yield return new WaitForSeconds((show.growSpeed / 100) + show.CurrentTime);
        }

    }



}
