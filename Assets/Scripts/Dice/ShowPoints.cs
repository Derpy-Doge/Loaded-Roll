using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowPoints : MonoBehaviour
{
    [Header("Required Stuff")]
    public TMP_Text pointText;
    public TMP_Text CalcTexts;
    public DiceScoreCalc Calc;
    public Animator pointanim;
    //[SerializeField]private Slider speedSlider;
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
    [Space]
    [Header("SpeedUp")]
    [SerializeField]private Vector3 mousePosition;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private RectTransform speedTextLocation;
    [SerializeField]private RectTransform rect;
    [SerializeField] private GameObject GameManagerGO;

    private float colorPoints;

    private bool spawned = false;

    public bool textFinished;

    private bool speedUp;
    [HideInInspector] public bool calcdone;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //speedSlider.value = growSpeed;
        //speedSlider.onValueChanged.AddListener(delegate { UpdateSpeed(speedSlider.value); });
        //CurrentTime = (3 - (growSpeed / 100));
        speed = 0.5f;
        saveSpeed = speed;
        Timer = CurrentTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = growSpeed.ToString() + "x";
        pointanim.speed = growSpeed;
        if (growSpeed < 1)
            growSpeed = 1;

        //mousePosition = Input.mousePosition;
        //speedText.transform.position = mousePosition + new Vector3(0f, 50f, 0f);
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out pos);
        pos = pos + new Vector2(0f, 50f);
        speedTextLocation.anchoredPosition = pos;


    }
    public void SpeedUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            speedUp = true;
            speedText.gameObject.SetActive(true);
        }
        else if (ctx.canceled)
        {
            speedUp = false;
            speedText.gameObject.SetActive(false);

        }
    }
    public void Scroll(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && speedUp)
        {
            growSpeed = Mathf.Clamp(growSpeed + ctx.ReadValue<Vector2>().y, 1f, 5f);
            UpdateSpeed();
        }
    }

    private void UpdateSpeed()
    {
        //growSpeed = newvalue; 
        
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


    public IEnumerator TextCalc(float amount, string CalcText)
    {
        colorPoints = amount;
            //pointText.text = string.Empty;
            pointanim.SetTrigger("Write");
            yield return new WaitForSeconds(1f);
            CalcTexts.text = CalcText;
            pointText.text = amount.ToString();
            //pointText.color = Color.white;
            pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 1f, 1f));
            //ColorCalc();
            yield return new WaitForSeconds(speed);
            Debug.Log("Points added");
            Timer = CurrentTime;
            amount = 0;
             spawned = false;
             textFinished = false;
            Calc.showpoint = 0;
    }

    public IEnumerator TotalCalc(string CalcText)
    {
        colorPoints = Calc.addedPoints;

        pointText.text = string.Empty;
        pointanim.SetTrigger("Write");
        yield return new WaitForSeconds(1f);
        CalcTexts.text = CalcText;
        pointText.text = Calc.addedPoints.ToString();
        //pointText.color = Color.white;
        pointText.fontMaterial.SetVector("_GlowColor", new Vector4(1f, 1f, 1f, 1f));
        //ColorCalc();
        yield return new WaitForSeconds(speed);
        Debug.Log("Points added");
        GameManagerGO.SendMessage("AddPoints", Mathf.RoundToInt(Calc.addedPoints));

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
        //pointanim.SetTrigger("Write");
        yield return new WaitForSeconds(speed);
        Debug.Log("Points added");
        Timer = CurrentTime;
        Calc.zeros = 0;
        spawned = false;
        textFinished = false;
        calcdone = true;
        //pointanim.SetTrigger("Unwrite");
    }
}

