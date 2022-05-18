using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Idle;

    private float waitTime;

    private bool hasWaited;

    public override void EnterState()
    {
        agent.SetDestination(agent.transform.position);
        agent.animator.SetTrigger("idle");

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

        stateMachine.ChangeState(EnemyStates.Surprise);
    }

    public override void ExitState()
    {
        agent.animator.ResetTrigger("idle");
    }

    public void ResetState()
    {
        stateMachine.ChangeState(EnemyStates.Idle);
    }

    public void SelectNewDestination()
    {
        hasWaited = true;

        agent.SetDestination(stateMachine.moveSpots.GetNewSpot());
    }

    public void Patrol()
    {
        if (!agent.navMeshAgent.hasPath)
        {
            agent.animator.SetBool("moving", false);

            if (waitTime <= -1)
            {
                ResetState();
            }
        }
    }

}
