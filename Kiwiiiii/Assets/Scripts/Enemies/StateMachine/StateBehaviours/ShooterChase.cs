using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterChase : EnemyChase
{
    private Animator animator;

    public override void EnterChase()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("run");
    }
    public override void ActiveChase()
    {
        return;
    }
    public override void ExitChase()
    {
        animator.SetTrigger("idle");
    }
}
