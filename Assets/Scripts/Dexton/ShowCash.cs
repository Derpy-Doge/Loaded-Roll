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
        if (whatToShow == WhatToShow.DebtInstallment)
        {
            GetComponent<TMPro.TMP_Text>().SetText(string.Format(format, SaveDataController.Instance.current.run.CurrentInstallment));
        }
        else if (whatToShow == WhatToShow.TotalCash)
        {
            GetComponent<TMPro.TMP_Text>().SetText(string.Format(format, FindAnyObjectByType<DiceScoreCalc>().points));
        }
        else if (whatToShow == WhatToShow.RestockCost)
        {
            GetComponent<TMPro.TMP_Text>().SetText(string.Format(format, FindAnyObjectByType<Shops>().RestockPrice)); 
        }
    }
    public enum WhatToShow
    {
        DebtInstallment,
        TotalCash,
        RestockCost

    }
}
