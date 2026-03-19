using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPoints : MonoBehaviour
{
    public GameObject panelText;
    private TMP_Text pointText;

    [Header("Calc Script")]
    public DiceScoreCalc Calc;

    [Header("Spawner Script")]
    public Spawner Spawn;
    [Space]
    private float Timer;
    public float CurrentTime;

    public float growSpeed;

    private float startSize = 0f;
    public float endSize;

    private bool spawned = false;

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
            pointText.color = Color.green;
        }
        else if (Calc.addedPoints <= 5000 && Calc.addedPoints >= 1000)
        {
            pointText.color = Color.red;
        }

        else if (Calc.addedPoints <= 10000 && Calc.addedPoints > 5000)
        {
            pointText.color = Color.blue;
        }

        else if (Calc.addedPoints <= 20000 && Calc.addedPoints > 10000)
        {
            pointText.color = Color.yellow;
        }

        else if (Calc.addedPoints <= 30000 && Calc.addedPoints > 20000)
        {
            pointText.color = Color.magenta;
        }

           else if (Calc.addedPoints <= 40000 && Calc.addedPoints > 30000)
            {
                pointText.color = Color.cyan;
        }
    }

    private void TextCalc()
    { 

        if (Calc.addedPoints > 0)
            {
            if (!spawned)
            {
                StartCoroutine(Spawn.SpawnText());
                pointText = panelText.GetComponentInChildren<TMP_Text>();
                pointText.text = string.Empty;
                pointText.text += Calc.addedPoints.ToString();
                Debug.Log("point text now shows points");
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

            //Debug.Log(pointText.color.a);
            if (pointText.color.a > startSize)
            {
                pointText.color = new(pointText.color.r, pointText.color.g, pointText.color.b, pointText.color.a - 10f * Time.deltaTime);
            }
           else
           { 
                pointText.text = string.Empty;
                pointText.color = Color.white;
                Timer = CurrentTime;
                spawned = false;
                var textObj = GameObject.FindGameObjectWithTag("Points");
                Destroy(textObj, 0f);
           }
            

            

        }
    }
}
