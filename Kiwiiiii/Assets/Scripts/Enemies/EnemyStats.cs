using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public SOEnemyStats stats;
    public int healthAmount;

    void Start()
    {
        stats = Instantiate(stats);
        stats.health = healthAmount;
    }
}
