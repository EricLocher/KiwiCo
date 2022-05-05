using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public SOCharacterStats CharacterStats;

    void Start()
    {
        CharacterStats = Instantiate(CharacterStats);
    }

    public virtual void DealDamage(float value)
    {
        CharacterStats.health -= value;

        if(CharacterStats.health <= 0) { OnDeath(); }
    }

    public virtual void Heal(float value)
    {
        CharacterStats.health += value;
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}
