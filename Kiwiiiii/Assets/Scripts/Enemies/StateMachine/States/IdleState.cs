using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Idle;

    public override void EnterState()
    {
        return;
    }

    public override void Update()
    {
        if (!agent.fov.TargetInView(agent.target)) { return; }

        stateMachine.ChangeState(EnemyStates.Chase);
    }

    public override void ExitState()
    {
        return;
    }

}
