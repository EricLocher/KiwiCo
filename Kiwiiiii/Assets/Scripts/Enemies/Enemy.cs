using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[Serializable, RequireComponent(typeof(NavMeshAgent), typeof(FOV), typeof(PatrolSpots))]
public class Enemy : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public VisualEffect AppearEffect;
    
    public EnemyAttack attack;
    public EnemyChase chase;
    public EnemyStats stats;
    public EnemyStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public FOV fov;

    void Awake()
    {
        fov = GetComponent<FOV>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.moveSpots = GetComponent<PatrolSpots>();
        stateMachine.enemy = this;

        stateMachine.RegisterState(EnemyStates.Idle, new IdleState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Chase, new ChaseState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Attack, new AttackState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Death, new DeathState(this, stateMachine));

        stateMachine.ChangeState(EnemyStates.Idle);
    }

    public void SetDestination(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    private void OnEnable()
    {
        AppearEffect.Play();
    }

    private void Update()
    {
        if (stats.stats.health <= 0)
        {
            stateMachine.ChangeState(EnemyStates.Death);
        }

        stateMachine.Update();
    }
}


