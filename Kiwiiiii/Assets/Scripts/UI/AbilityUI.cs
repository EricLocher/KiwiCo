using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] AbilityButton abilityButton;
    AbilityHolder AbilityHolder;

    private void Awake()
    {
        AbilityHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityHolder>();
    }

    private void Start()
    {
        foreach (Ability ability in AbilityHolder.abilities) {
            var button = Instantiate(abilityButton, transform);
            button.ability = ability;
        }
    }


}
