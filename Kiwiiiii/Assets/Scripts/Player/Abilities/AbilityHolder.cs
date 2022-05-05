using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public List<Ability> abilities;

    void Awake()
    {
        foreach (Ability ability in abilities) {
            ability.Init(rb);
        }
    }

    void Update()
    {
        foreach (Ability ability in abilities) {
            ability.UpdateAbility(Time.deltaTime);
        }
    }


}
