using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShowPoints : MonoBehaviour
{
    [Header("Required Stuff")]
    public TMP_Text pointText;
    public TMP_Text CalcTexts;
    public DiceScoreCalc Calc;
    public Spawner Spawn;
    [Space]
    [Header("How Long Should Text Pop-Up")]
    private float Timer;
    public float CurrentTime;
    [Space]
    [Header("How Fast Should Text Pop-Up")]
    [Range(200,10000)]public float growSpeed;
    [Space]
    [Header("How large Should It Be")]
    private float startSize = 0f;
    public float endSize;

    private float colorPoints;

    private bool spawned = false;

    public bool textFinished;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = CurrentTime;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void ColorCalc()
    {
        if (colorPoints == 0)

        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 0f, 0f, 1f));
            pointText.color = Color.red;
        }

        else if (colorPoints < 1000 && colorPoints >= 0)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(0f, 1f, 0f, 1f));
        }
        else if (colorPoints <= 5000 && colorPoints >= 1000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, .5f, .5f, 1f));
        }

        else if (colorPoints <= 10000 && colorPoints > 5000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(0f, 0f, 1f, 1f));
        }

        else if (colorPoints <= 20000 && colorPoints > 10000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 0f, 1f));
        }

        else if (colorPoints <= 30000 && colorPoints > 20000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 0f, 1f, 1f));
        }

        else if (colorPoints <= 40000 && colorPoints > 30000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(0f, 1f, 1f, 1f));
        }
    }

    private IEnumerator CalcTextEnter()
    {
        while (CalcTexts.fontSize < endSize)
        {
            CalcTexts.fontSize += growSpeed * Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator CalcTextExit()
    {
        while (CalcTexts.fontSize > startSize)
        {
            CalcTexts.fontSize -= (growSpeed / 10f) * Time.deltaTime;
            yield return null;
        }
    }


    public IEnumerator TextCalc(float amount)
    {
        colorPoints = amount;
        while (textFinished)
        {
            if (!spawned)
            {
                pointText.text = string.Empty;
                pointText.text += amount.ToString();
                pointText.color = Color.white;
                pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 1f, 1f));
                ColorCalc();
                StartCoroutine(CalcTextEnter());
                spawned = true;

            }



            if (pointText.fontSize < endSize)
            {
                pointText.fontSize += growSpeed * Time.deltaTime;
            }

            else if (amount != 0)
            {
                Debug.Log("Points added");
                Calc.addedPoints += amount;
                Timer = CurrentTime;
                amount = 0;
            }

        
             if (pointText.fontSize >= endSize && amount == 0)
            {
                Timer -= Time.deltaTime;
            }


            if (Timer <= 0 && amount == 0)
            {
                Debug.Log("byebye");
                StartCoroutine(CalcTextExit());
                if (pointText.fontSize > startSize && amount == 0)
                {
                    pointText.fontSize -= (growSpeed * 2) * Time.deltaTime;
                }
                else
                {
                    pointText.text = string.Empty;
                    Timer = CurrentTime;
                    spawned = false;
                    Calc.showpoint = 0f;
                    textFinished = false;

                }

            }
            yield return null;
        }
    }

    public IEnumerator TotalCalc()
    {
        colorPoints = Calc.addedPoints;
        while (textFinished)
        {
            if (!spawned)
            {
                pointText.text = string.Empty;
                pointText.text += Calc.addedPoints.ToString();
                pointText.color = Color.white;
                ColorCalc();
                StartCoroutine(CalcTextEnter());
                spawned = true;

            }



            if (pointText.fontSize < endSize)
            {
                pointText.fontSize += growSpeed * Time.deltaTime;
            }
            else
            {
                Timer = CurrentTime;
            }

            if (pointText.fontSize >= endSize)
            {
                Timer -= Time.deltaTime;
            }


            if (Timer <= 0)
            {
                StartCoroutine(CalcTextExit());
                if (pointText.fontSize > startSize + 1)
                {
                    pointText.fontSize -= (growSpeed * 2) * Time.deltaTime;
                }
                else
                {
                    pointText.text = string.Empty;
                    Timer = CurrentTime;
                    spawned = false;
                    textFinished = false;
                    Calc.points += Calc.addedPoints;

                }

            }
            yield return null;
        }
    }
}

