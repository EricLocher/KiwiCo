using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable, RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : FOV
{
    [Header("Target")]
    [SerializeField] Transform target;
    [Header("Stats")]
    [SerializeField] float health;
    [SerializeField] float moveSpeed;
    [Range(0, 10)] public float attackDistance;

    EnemyStates state = EnemyStates.Idle;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackDistance;
    }

    void Update()
    {
        Track();
    }

    protected abstract void Attack();
    protected void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) {
            health = 0;
        }
    }

    protected virtual void Track()
    {
        if (Vector3.Distance(transform.position, target.position) < attackDistance) {
            Attack();
        }
        else if (TargetInView(target)) {
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


