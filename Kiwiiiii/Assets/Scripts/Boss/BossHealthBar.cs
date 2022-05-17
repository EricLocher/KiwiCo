using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider bossHealth, bossShield;
    public SOBossStats stats;


    private void Start()
    {
        bossHealth.maxValue = stats.maxHealth;
        bossShield.maxValue = stats.maxShield;
    }

    private void Update()
    {
        bossHealth.value = stats.health;
        bossShield.value = stats.shield;
    }
}
