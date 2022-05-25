using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text count;
    [SerializeField] Image img;
    public Ability ability;

    bool blinking = false;

    private void Start()
    {
        img.sprite = ability.icon;
        slider.maxValue = ability.coolDownTime;

        ability.onUseEvent += OnUse;

        if(ability.maxAmount == 1) {
            count.enabled = false;
        }
    }

    private void Update()
    {
        slider.value = ability.timer;
        if (!count.enabled) { return; }
        count.text = $"{ability.currentAmount}";
    }

    private void OnUse(bool couldUse)
    {
        if(couldUse || blinking) { return; }
        StartCoroutine(BlinkRed(0.1f));
    }

    IEnumerator BlinkRed(float blinkTime)
    {
        blinking = true;

        float timeElapsed = 0;

        AudioManager.instance.PlayOnce("Cooldown");

        while (timeElapsed <= blinkTime) {
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
            img.color = new Color(1,  1 - timeElapsed / blinkTime, 1 - timeElapsed / blinkTime, 1);
        }

        img.color = new Color(1, 1, 1, 1);

        blinking = false;

        yield return null;
    }

    private void OnDestroy()
    {
        ability.onUseEvent -= OnUse;
    }

}

