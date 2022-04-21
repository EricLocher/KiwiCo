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

    private EnemyStats enemyStats;

    public delegate void EnemyDeath();
    public static event EnemyDeath death;

    void Awake()
    {
        fov = GetComponent<FOV>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();

        stateMachine = new EnemyStateMachine(this);
        stateMachine.RegisterState(EnemyStates.Idle, new IdleState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Chase, new ChaseState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Attack, new AttackState(this, stateMachine));

        stateMachine.ChangeState(EnemyStates.Idle);

        stateMachine.Update();
    }   

    void Update()
    {
        stateMachine.Update();

        if (enemyStats.stats.health <= 0)
        {
            death?.Invoke();
        }
    }

    public void SetDestination(Transform target)
    {
        navMeshAgent.SetDestination(target.position);
    }

    void Death()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        death += Death;
    }

    private void OnDisable()
    {
        death -= Death;
    }

}


