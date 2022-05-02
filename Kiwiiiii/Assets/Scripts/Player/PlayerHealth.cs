using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Slider healthSlider;

    void Start()
    {
        if (healthSlider != null)
            healthSlider.value = soPlayerStats.health;
    }

    public void Heal(float healAmt)
    {
        if(healthSlider != null)
            healthSlider.value = soPlayerStats.health += healAmt;
    }
}