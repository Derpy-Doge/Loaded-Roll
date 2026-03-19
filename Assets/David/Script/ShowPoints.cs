using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPoints : MonoBehaviour
{
    public GameObject panelText;
    public TMP_Text pointText;

    [Header("Calc Script")]
    public DiceScoreCalc Calc;

    [Header("Spawner Script")]
    public Spawner Spawn;
    [Space]
    public float Timer;
    private float CurrentTime;

    private float startSize = 0f;
    public float endSize;

    private bool spawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentTime = Timer;

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
            if (!spawned)
            {
                StartCoroutine(Spawn.SpawnText());
                pointText = panelText.GetComponentInChildren<TMP_Text>();
                pointText.text = null;
                pointText.text += Calc.addedPoints.ToString();
                Debug.Log("point text now shows points");
                ColorCalc();
                Timer -= Time.deltaTime;
                spawned = true;
            }

            else if (Timer > 0 && pointText.fontSize != endSize)
            {
                pointText.fontSize += 1;
            }

            else if (Timer <= 0)
            { 
                Timer = CurrentTime;
                Debug.Log("Points added: " + Calc.addedPoints);
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
            pointText.IsDestroyed();
            spawned = false;
        }
    }
}
