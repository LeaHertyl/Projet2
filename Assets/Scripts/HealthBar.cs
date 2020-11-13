﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetHeatlh(int health)
    {
        slider.value = health;
    }

    private void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
