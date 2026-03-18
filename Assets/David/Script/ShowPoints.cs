using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPoints : MonoBehaviour
{
    public TMP_Text pointText;

    public DiceScoreCalc Calc;

    public float Timer;
    private float CurrentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentTime = Timer;
        pointText.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Calc.addedPoints > 0)
        {
            pointText.text += Calc.addedPoints.ToString();
            Calc.addedPoints = 0;

        }

        else if (pointText != null)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0 && Calc.addedPoints == 0)
        { 
            pointText.text = null;
        }
    }
}
