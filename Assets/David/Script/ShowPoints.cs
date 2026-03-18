using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPoints : MonoBehaviour
{
    public TMP_Text pointText;

    public DiceScoreCalc Calc;

    public float Timer;
    private float CurrentTime;

    private float startSize = 0f;
    public float endSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentTime = Timer;
        pointText.text = null;
        pointText.fontSize = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        TextCalc();
    }


    private void ColorCalc()
    { 
        if (Calc.addedPoints < 1000 && Calc.addedPoints >= 0)
        {
            pointText.color = Color.green;
        }
        else if (Calc.addedPoints <= 5000 && Calc.addedPoints >= 1000)
        {
            pointText.color = Color.red;
        }
    }

    private void TextCalc()
    { 
    if (Calc.addedPoints > 0)
        {
            if (pointText.text == null)
            {
            pointText.text += Calc.addedPoints.ToString();
            ColorCalc();

            }

            Timer -= Time.deltaTime;
            if (Timer > 0 && pointText.fontSize != endSize)
            {
                pointText.fontSize += 1;
            }
            else
            { 
                Timer = CurrentTime;
                Calc.points += Calc.addedPoints;
                Calc.addedPoints = 0;
                
            }

        }

        else if (pointText.text != null)
        {
            Timer -= Time.deltaTime;
        }


        if (Timer <= 0 && Calc.addedPoints == 0)
        {
            pointText.text = null;
            Timer = CurrentTime;
            pointText.fontSize = startSize;
            pointText.color = Color.white;
        }
    }
}
