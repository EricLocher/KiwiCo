using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable, RequireComponent(typeof(NavMeshAgent), typeof(FOV))]
public abstract class Enemy : MonoBehaviour
{

    [Header("FOV")]
    public FOV fov;
    [SerializeField] Transform target;
    [Header("Stats")]
    [SerializeField] float health;
    [SerializeField] float moveSpeed;

    NavMeshAgent navMeshAgent;

    void Start()
    {
        if(fov is null)
            fov = GetComponent<FOV>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = fov.innerRadius;
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
        if (Vector3.Distance(transform.position, target.position) < fov.innerRadius) {
            Attack();
        }
        else if (fov.TargetInView(target)) {
            navMeshAgent.SetDestination(target.position);
        }
    }
}


