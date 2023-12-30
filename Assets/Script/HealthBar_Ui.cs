using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar_Ui : MonoBehaviour
{
    private Entity entity;
    private Character_Starts myStart;
    
    private RectTransform myTransform;
    private Slider slider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStart = GetComponentInParent<Character_Starts>();
        

        entity.onFlipped += FlipUi;
        myStart.onHealthChanged += UpdateHealthUI;
        
        UpdateHealthUI();
        
        Debug.Log("Health BAR UI Called");
    }

    

    private void UpdateHealthUI()
    {
        slider.maxValue = myStart.GetMaxHealthValue();
        slider.value = myStart.currentHealt;
    }
    
    private void FlipUi() =>  myTransform.Rotate(0,180,0);
    private void OnDisable()
    {
        entity.onFlipped -= FlipUi;
        
        myStart.onHealthChanged -= UpdateHealthUI;
    }


}
