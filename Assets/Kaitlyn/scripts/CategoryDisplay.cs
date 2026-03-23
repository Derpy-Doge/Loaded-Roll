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
    }

    
    void Update()
    {
        
    }
}
