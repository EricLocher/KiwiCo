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

    private void Start()
    {
        img.sprite = ability.icon;
        slider.maxValue = ability.coolDownTime;
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
}

