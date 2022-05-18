using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Slider hp;
    Boss agent;

    private void Start()
    {
        agent = GetComponent<Boss>();
        hp.maxValue = agent.stats.maxHealth;
        hp.value = agent.stats.maxHealth;
    }
    private void Update()
    {
        //Test: UpdateHealthBar(1);
    }
    public void UpdateHealthBar(float damage)
    {
        hp.value -= damage;
    }
}