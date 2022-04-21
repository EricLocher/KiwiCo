using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private EnemyStats enemyStats;
    private PlayerController playerStats;

    void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<EnemyStats>();
        playerStats = GetComponent<PlayerController>();
    }

    public void TakeDamage()
    {
        playerStats.playerStats.health--;
        Debug.Log(("Enemy did damage, current player health: ") + playerStats.playerStats.health);
    }

    public void DoDamage()
    {
        enemyStats.stats.health--;
        Debug.Log(("Player did damage, current enemy health: ") + enemyStats.stats.health);
    }

    public void Heal(float healAmt)
    {
        playerStats.playerStats.health += healAmt;
    }
}