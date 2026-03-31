using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static GameManager;

public class RollWarnings : MonoBehaviour
{
    [SerializeField] private List<GameObject> warnings;
    [SerializeField] private GameObject selectedWarning;
    [SerializeField] private RollDice rollDice;
    [SerializeField] private GameManager gm;

    void Start()
    {
        rollDice = RollDice.Instance;
        gm = GameManager.Instance;

        GameObject warningsParent = GameObject.FindGameObjectWithTag("Warnings");
        if (warningsParent != null)
        {          
            foreach (Transform child in warningsParent.transform)
            {
                warnings.Add(child.gameObject);
            }

            foreach(GameObject obj in warnings)
            {
                obj.SetActive(false);
            }
        }

        selectedWarning = null;
    }

    void Update()
    {
        if (gm.rolls + 1 > 3) //New Round || ROLLS PER ROUND IS PRIVATE WHY IS IT PRIVATE
        {
            selectedWarning = warnings[0];
        }

        else if (gm.CurrentState != GameStates.Rolling) //Curent state isnt rolling
        {
            selectedWarning = warnings[1];
        }
        else if (RollDice.Instance.diceTextures.CheckNulls())
        {
            selectedWarning = warnings[2];
        }
    }

    public void DisplayWarning()
    {
        StartCoroutine(WarningDispaly());
    }

    public IEnumerator WarningDispaly()
    {
        if (selectedWarning != null)
        {
            selectedWarning.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            selectedWarning.SetActive(false);
        }
    }
}
