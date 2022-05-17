using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider bossHealth;
    public SOBossStats stats;


    private void Start()
    {
        bossHealth.maxValue = stats.maxHealth;
    }

    private void Update()
    {
        bossHealth.value = stats.health;
    }
}
