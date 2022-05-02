using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private EnemyStats enemyStats;
    private PlayerController playerStats;
    public Slider healthSlider;

    void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<EnemyStats>();
        playerStats = GetComponent<PlayerController>();
        if (healthSlider != null)
            healthSlider.value = playerStats.playerStats.health;
    }

    public void TakeDamage(float damage)
    {

        playerStats.playerStats.health = playerStats.playerStats.health - damage;

        if (healthSlider != null)
            healthSlider.value = playerStats.playerStats.health;
    }

    public void DoDamage(float damage)
    {
        enemyStats.stats.health -= damage;
    }

    public void Heal(float healAmt)
    {
        if(healthSlider != null)
            healthSlider.value = playerStats.playerStats.health += healAmt;
    }
}