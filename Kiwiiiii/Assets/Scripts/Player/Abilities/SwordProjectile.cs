using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordProjectile", menuName = "Utilities/Abilities/SwordProjectile")]
public class SwordProjectile : Ability
{

    [Header("Sword Projectile Specific Settings")]
    [SerializeField] SlashProjectile projectilePrefab;
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float lifeTime;


    public override void DoAbility()
    {
        SlashProjectile projectile = Instantiate(projectilePrefab);
        projectile.Slash(sword, speed, damage, lifeTime);
    }
}
