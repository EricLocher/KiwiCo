using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] ControlWeapon sword;
    public List<Ability> abilities;

    void Awake()
    {
        for (int i = 0; i < abilities.Count; i++) {
            abilities[i] = Instantiate(abilities[i]);

            abilities[i].Init(movement, sword);
        }
    }

    void Update()
    {
        foreach (Ability ability in abilities) {
            ability.UpdateAbility(Time.deltaTime);
        }
    }
}
