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
    public float growSpeed;
    [Space]
    [Header("How large Should It Be")]
    private float startSize = 0f;
    public float endSize;

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
        if (Calc.addedPoints < 1000 && Calc.addedPoints >= 0)
        {
        pointText.fontMaterial.SetVector("_GlowColor", new Vector4(0f, 1f, 0f, 1f));
        }
        else if (Calc.addedPoints <= 5000 && Calc.addedPoints >= 1000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 0f, 0f, 1f));
        }

        else if (Calc.addedPoints <= 10000 && Calc.addedPoints > 5000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(0f, 0f, 1f, 1f));
        }

        else if (Calc.addedPoints <= 20000 && Calc.addedPoints > 10000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 0f, 1f));
        }

        else if (Calc.addedPoints <= 30000 && Calc.addedPoints > 20000)
        {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 0f, 1f, 1f));
        }

           else if (Calc.addedPoints <= 40000 && Calc.addedPoints > 30000)
            {
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(0f, 1f, 1f, 1f));
        }
    }

    public IEnumerator CalcTextEnter()
    {
        while (CalcTexts.fontSize < endSize)
        {
            CalcTexts.fontSize += growSpeed * Time.deltaTime;
        }
        yield return new WaitForSeconds((growSpeed / 100) + CurrentTime);
    }

    public IEnumerator CalcTextExit()
    {
        while (CalcTexts.fontSize <= startSize)
        {
            CalcTexts.fontSize -= growSpeed * Time.deltaTime;
        }
        yield return new WaitForSeconds((growSpeed / 100) + CurrentTime);
    }
    public IEnumerator TextCalc(float amount)
    {
        while (textFinished)
        {
            if (amount > 0)
            {
                if (!spawned)
                {
                    pointText.text = string.Empty;
                    pointText.text += amount.ToString();
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
                    Debug.Log("Points added: " + amount);
                    Calc.points += amount;
                    Timer = CurrentTime;
                    amount = 0;
                }

            }
            else if (amount == 0 && pointText.fontSize >= endSize)
            {
                Timer -= Time.deltaTime;
            }


            if (Timer <= 0 && amount == 0)
            {

                if (pointText.fontSize > startSize + 1)
                {
                    pointText.fontSize -= growSpeed * Time.deltaTime;
                }
                else
                {
                    pointText.text = string.Empty;
                    Timer = CurrentTime;
                    spawned = false;
                    textFinished = false;

                }

            }
            yield return null;
        }
    }
}
