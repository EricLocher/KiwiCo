using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAttack : EnemyAttack
{
    private Animator animator;

    public override void EnterAttack()
    {
        animator = GetComponent<Animator>();
        return;
    }

    public override void ActiveAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitAttack()
    {
        throw new System.NotImplementedException();
    }
}
