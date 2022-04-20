using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable, RequireComponent(typeof(NavMeshAgent), typeof(FOV))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public Transform target;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public FOV fov;

    protected EnemyStateMachine stateMachine;

    void Awake()
    {
        fov = GetComponent<FOV>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }   
    void Start()
    {
        stateMachine = new EnemyStateMachine(this);
        stateMachine.RegisterState(EnemyStates.Idle, new IdleState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Chase, new ChaseState(this, stateMachine));

        stateMachine.ChangeState(EnemyStates.Idle);

        stateMachine.Update();
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void SetDestination(Transform target)
    {
        navMeshAgent.SetDestination(target.position);
    }

}


