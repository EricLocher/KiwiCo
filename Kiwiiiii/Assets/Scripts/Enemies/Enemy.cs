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

<<<<<<< Updated upstream
=======
    private SOEnemyStats stats;

    public delegate void EnemyDeath();
    public static event EnemyDeath death;

>>>>>>> Stashed changes
    void Awake()
    {
        fov = GetComponent<FOV>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stats = GetComponent<SOEnemyStats>(); 
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
<<<<<<< Updated upstream
=======

        if (stats.health <= 0)
        {
            death?.Invoke();
        }
>>>>>>> Stashed changes
    }

    public void SetDestination(Transform target)
    {
        navMeshAgent.SetDestination(target.position);
    }

}


