using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Idle;

    private int randomDestinationSpot;

    public override void EnterState()
    {
        agent.SetDestination(agent.transform);
    }

    public override void Update()
    {
        if (!agent.fov.TargetInView(agent.target)) 
        {
            Patrol();
            return;
        }

        stateMachine.ChangeState(EnemyStates.Chase);
    }

    public override void ExitState()
    {
        return;
    }

    public void SelectNewDestination()
    {
        Debug.Log(stateMachine);
        //Select a new random point, avoid the one in use.
        int newRandomDestination = Random.Range(0, stateMachine.moveSpots.Length);

        while (randomDestinationSpot == newRandomDestination)
        {
            newRandomDestination = Random.Range(0, stateMachine.moveSpots.Length);
        }

        randomDestinationSpot = newRandomDestination;
        agent.SetDestination(stateMachine.moveSpots[randomDestinationSpot]);
    }

    public void Patrol()
    {
        if (!agent.navMeshAgent.hasPath)
        {
            SelectNewDestination();
        }
    }

}
