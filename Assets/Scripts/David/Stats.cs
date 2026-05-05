using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public TMP_Text stats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStats()
    {
        stats.text = $"Round: {SaveDataController.Instance.current.run.CurrentRound}\n" +"\n" +
            $"Installment: {SaveDataController.Instance.current.run.CurrentInstallment}\n" + "\n" +
            $"Points: {SaveDataController.Instance.current.run.Points}\n" + "\n" +
            $"Total Earned Points: {SaveDataController.Instance.current.run.TotalEarnedPoints}\n" + "\n" +
            $"Current Debt: {SaveDataController.Instance.current.run.CurrentDebt}\n" + "\n" +
            $"Current Interest Rate: {SaveDataController.Instance.current.run.CurrentInterestRate}%";
    }
}
