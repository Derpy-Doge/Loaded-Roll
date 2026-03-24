using System;
using System.Collections.Generic;
using UnityEngine;

public class CategoryDisplay : MonoBehaviour
{
    private DiceScoreCalc calc;

    [SerializeField] private GameObject categoryDisplayObject;
    [SerializeField] private List<GameObject> upperCategories;
    [SerializeField] private List<GameObject> lowerCategories;


    void Start()
    {
        categoryDisplayObject = this.gameObject;
        calc = DiceScoreCalc.Instance;
    }

    
    void Update()
    {
        #region upper categories 

        //all the lists for what you rolled are private :pensive:

        #endregion
    }
}
