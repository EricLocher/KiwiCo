using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public SOPlayerStats soPlayerStats;
    public SOEnemyStats soEnemyStats;

    public void TakeDamage(float health, float damage)
    {
        health -= damage;
    }

    public void DoDamage(float health, float damage)
    {
        health -= damage;
    }

    public void Heal(float health, float healAmt)
    {
        health += healAmt;
    }
}
