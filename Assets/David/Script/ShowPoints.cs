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
    public Animator pointanim;
    public Animator textanim;
    [SerializeField]private Slider speedSlider;
    [Space]
    [Header("How Long Should Text Pop-Up")]
    private float Timer;
    public float CurrentTime;
    public float speed = .1f;
    private float saveSpeed;
    [Space]
    [Header("How Fast Should Text Pop-Up(in percent)")]
    public float growSpeed;
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
        speedSlider.value = growSpeed;
        speedSlider.onValueChanged.AddListener(delegate { UpdateSpeed(speedSlider.value); });
        //CurrentTime = (3 - (growSpeed / 100));
        speed = 0.5f;
        saveSpeed = speed;
        Timer = CurrentTime;
    }

    // Update is called once per frame
    void Update()
    {
        pointanim.speed = growSpeed;
        textanim.speed = growSpeed;
        if (growSpeed < 1)
            growSpeed = 1;

       

        
    }

    private void UpdateSpeed(float newvalue)
    {
        growSpeed = newvalue; 
        
        if (growSpeed == 1)
        {
            CurrentTime = Timer;
            speed = saveSpeed;
            
        }

        else
        {
            CurrentTime = Timer;
            speed = saveSpeed;
            CurrentTime /= growSpeed;
            speed /= growSpeed;
        }
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


    public IEnumerator TextCalc(float amount)
    {
        colorPoints = amount;
            //pointText.text = string.Empty;
            pointText.text = amount.ToString();
            pointText.color = Color.white;
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 1f, 1f));
            ColorCalc();
            pointanim.SetTrigger("Grow");
            textanim.SetTrigger("GrowText");
            yield return new WaitForSeconds(speed);
            Debug.Log("Points added");
            Calc.addedPoints += amount;
            Timer = CurrentTime;
            amount = 0;
             spawned = false;
             Calc.showpoint = 0f;
             textFinished = false;
    }

    public IEnumerator TotalCalc()
    {
        colorPoints = Calc.addedPoints;

        //pointText.text = string.Empty;
        pointText.text = Calc.addedPoints.ToString();
        pointText.color = Color.white;
        pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 1f, 1f));
        ColorCalc();
        pointanim.SetTrigger("Grow");
        textanim.SetTrigger("GrowText");
        yield return new WaitForSeconds(speed);
        Debug.Log("Points added");
        Calc.points += Calc.addedPoints;
        Timer = CurrentTime;
        Calc.addedPoints = 0;
        spawned = false;
        textFinished = false;

    }

    public IEnumerator TotalZeroCalc()
    {
        colorPoints = 0;
                //pointText.text = string.Empty;
        pointText.text = Calc.zeros.ToString();
        pointText.color = Color.white;
        pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 1f, 1f));
        ColorCalc();
        pointanim.SetTrigger("Grow");
        textanim.SetTrigger("GrowText");
        yield return new WaitForSeconds(speed);
        Debug.Log("Points added");
        Timer = CurrentTime;
        Calc.zeros = 0;
        spawned = false;
        textFinished = false;
    }
}

