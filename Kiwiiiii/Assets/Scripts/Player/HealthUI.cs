using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image fillImage;
    PlayerController player;
    [SerializeField] Image blink;

    bool flashing = false;
    float lastFrame = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lastFrame = player.stats.health;
    }

    void Update()
    {
        fillImage.fillAmount = (player.stats.health / 300f);

        if(player.stats.health < lastFrame) {
            OnHit();
        }

        lastFrame = player.stats.health;
    }

    private void OnHit()
    {
        if(flashing) { return; }
        StartCoroutine(BlinkHealth());
    }

    IEnumerator BlinkHealth()
    {
        float blinkTime = 0.2f;
        float timeElapsed = 0;
        flashing = true;

        while (timeElapsed <= blinkTime) {
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
            blink.color = new Color(1, 1, 1, timeElapsed / blinkTime);
        }

        blink.color = new Color(1, 1, 1, 0);

        flashing = false;
        yield return null;
    }


}