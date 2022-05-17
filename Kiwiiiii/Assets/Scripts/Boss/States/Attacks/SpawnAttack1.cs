using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Utilities/BossAttacks/ProjectileAttack")]
public class ProjectileAttack : BossAttack
{

    [SerializeField] GameObject projectile;
    [SerializeField] int amount = 5; 

    public override void EnterState(BossPhase phase)
    {
        base.EnterState(phase);

        Debug.Log(currentPhase);
    }

    public override void ExitState()
    {
        currentPhase.RemoveSubState(this);
    }
}
