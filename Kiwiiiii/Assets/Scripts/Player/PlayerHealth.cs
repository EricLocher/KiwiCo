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

    public void TakeDamage()
    {
        if (healthSlider != null)
            healthSlider.value = playerStats.playerStats.health--;
        Debug.Log(("Enemy did damage, current player health: ") + playerStats.playerStats.health);
    }

    public void DoDamage()
    {
        enemyStats.stats.health--;
        Debug.Log(("Player did damage, current enemy health: ") + enemyStats.stats.health);
    }

    public void Heal(float healAmt)
    {
        if(healthSlider != null)
            healthSlider.value = playerStats.playerStats.health += healAmt;
    }
}