using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] Slider hp;
    [SerializeField] Image blink;
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


    public void OnHit()
    {
        StartCoroutine(BlinkHealth());
    }

    IEnumerator BlinkHealth()
    {
        float blinkTime = 0.2f;
        float timeElapsed = 0;

        while (timeElapsed <= blinkTime) {
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
            blink.color = new Color(1, 1, 1, timeElapsed / blinkTime);
        }

        blink.color = new Color(1, 1, 1, 0);
        yield return null;
    }

}