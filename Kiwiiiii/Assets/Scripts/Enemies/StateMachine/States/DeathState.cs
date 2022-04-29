using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    public DeathState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Death;

    public override void EnterState()
    {
        Object.Destroy(agent.gameObject);
    }

    public override void Update(float dt)
    {
        return;
    }

    public override void ExitState()
    {
        return;
    }
}
