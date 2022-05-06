using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChampChase : EnemyChase
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public override void EnterChase()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        animator.SetFloat("movement", navMeshAgent.speed);
        animator.speed = 3;
    }

    public override void ActiveChase()
    {
        return;
    }

    public override void ExitChase()
    {
        animator.speed = 1;
    }
}
