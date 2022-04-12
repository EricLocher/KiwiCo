using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable, RequireComponent(typeof(NavMeshAgent))]
public class Enemy : FOV
{
    [SerializeField] Transform target;
    EnemyStates state = EnemyStates.Idle;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        if (TargetInView(target)) {
            navMeshAgent.SetDestination(target.position);
        }
    }
}

enum EnemyStates
{
    Idle,
    Chasing,
    Attacking
}


