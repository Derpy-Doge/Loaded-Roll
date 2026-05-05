using UnityEngine;

public class ShowCash : MonoBehaviour
{
    public string format;
    public WhatToShow whatToShow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (whatToShow == WhatToShow.DebtInstalment)
        {
            GetComponent<TMPro.TMP_Text>().SetText(string.Format(format, FindAnyObjectByType<DiceScoreCalc>().points /*SaveDataController.current.run.CurrentDebt*/)); //alex this is the place holder 
        }
        else if (whatToShow == WhatToShow.TotalCash)
        {
            GetComponent<TMPro.TMP_Text>().SetText(string.Format(format, FindAnyObjectByType<DiceScoreCalc>().points));
        }
    }
    public enum WhatToShow
    {
        DebtInstalment,
        TotalCash

    }
}
