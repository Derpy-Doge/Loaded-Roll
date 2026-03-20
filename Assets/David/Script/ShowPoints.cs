using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPoints : MonoBehaviour
{
    [Header("Required Stuff")]
    public TMP_Text pointText;
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

    [HideInInspector] public bool textFinished;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = CurrentTime;

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

    private void TextCalc()
    { 

        if (Calc.addedPoints > 0)
            {
            if (!spawned)
            {
                pointText.text = string.Empty;
                pointText.text += Calc.addedPoints.ToString();
                pointText.color = Color.white;
                ColorCalc();
                spawned = true;
            }

            

            if (pointText.fontSize < endSize)
            {
                pointText.fontSize += growSpeed * Time.deltaTime;
            }

            else
            { 
                Debug.Log("Points added: " + Calc.addedPoints);
                Calc.points += Calc.addedPoints;
                Timer = CurrentTime;
                Calc.addedPoints = 0;
            }

            }
        else if (Calc.addedPoints == 0 && pointText.fontSize >= endSize)
        {
            Timer -= Time.deltaTime;
        }


        if (Timer <= 0 && Calc.addedPoints == 0)
        {

            if (pointText.fontSize > startSize)
            {
                pointText.fontSize -= growSpeed * Time.deltaTime;
            }
           else
           { 
                pointText.text = string.Empty;
                Timer = CurrentTime;
                spawned = false;;
           }
            
        }
    }
}
