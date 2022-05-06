using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChampIdle : EnemyIdle
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public override void EnterIdle()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void ActiveIdle()
    {
        animator.SetFloat("movement", navMeshAgent.speed);
    }

    public override void ExitIdle()
    {
        return;
    }
}
