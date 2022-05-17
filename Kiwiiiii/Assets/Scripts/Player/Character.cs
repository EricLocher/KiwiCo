using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public SOCharacterStats characterStats;

    void Awake()
    {
        characterStats = Instantiate(characterStats);
        Init();
    }

    protected virtual void Init() { }

    public virtual void DealDamage(float value)
    {
        characterStats.health -= value;
        AudioManager.instance.PlayOnce("PlayerTakeDamage");
        if (characterStats.health <= 0) { OnDeath(); }
    }

    public virtual void Heal(float value)
    {
        characterStats.health += value;
    }

    protected virtual void OnDeath()
    {
        AudioManager.instance.PlayOnce("GameOver");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Destroy(gameObject, 0.4f);
    }
}
