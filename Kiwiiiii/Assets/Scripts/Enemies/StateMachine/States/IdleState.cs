using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Idle;

    private int randomDestinationSpot;
    
    private float waitTime;

    private bool hasWaited;

    public override void EnterState()
    {
        agent.SetDestination(agent.transform.position);
        waitTime = 2;
        hasWaited = false;
    }

    public override void Update(float dt)
    {
        waitTime -= dt;

        if (waitTime <= 0 && hasWaited == false)
        {
            SelectNewDestination();
        }

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

    public void ResetState()
    {
        stateMachine.ChangeState(EnemyStates.Idle);
    }

    public void SelectNewDestination()
    {
        hasWaited = true;
        if (stateMachine.moveSpots.hasSpots)
            agent.SetDestination(stateMachine.moveSpots.GetNewSpot());
    }

    public void Patrol()
    {
        if (!agent.navMeshAgent.hasPath)
        {
            if (waitTime <= -1)
            {
                ResetState();
            }
        }
    }

}
