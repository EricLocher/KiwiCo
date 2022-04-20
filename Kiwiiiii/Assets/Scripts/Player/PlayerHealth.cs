using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private TestEnemy enemyStats;
    private PlayerMovement playerStats;

    void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<TestEnemy>();
        playerStats = GetComponent<PlayerMovement>();
    }

    public void TakeDamage()
    {
        playerStats.stats.health--;
        Debug.Log(("Enemy did damage, current player health: ") + playerStats.stats.health);
    }

    public void DoDamage()
    {
        enemyStats.stats.health--;
        Debug.Log(("Player did damage, current enemy health: ") + enemyStats.stats.health);
    }
}
