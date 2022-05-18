using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Slider hp;
    SOBossStats agent;

    private void Start()
    {
        agent = GetComponent<Boss>().stats;
        hp.maxValue = agent.maxHealth;
        hp.value = agent.maxHealth;
    }

    public void UpdateHealthBar(float hpAmt)
    {
        hp.value = hpAmt;
        //hp.value -= damage;
    }
}