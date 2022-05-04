using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerStats;
    public Slider healthSlider;

    void Start()
    {
        playerStats = GetComponent<PlayerController>();
        if (healthSlider != null)
            healthSlider.value = playerStats.stats.health;
    }

    public void TakeDamage(float damage)
    {

        playerStats.stats.health = playerStats.stats.health - damage;

        if (healthSlider != null)
            healthSlider.value = playerStats.stats.health;
    }

    public void Heal(float healAmt)
    {
        if(healthSlider != null)
            healthSlider.value = playerStats.stats.health += healAmt;
    }
}