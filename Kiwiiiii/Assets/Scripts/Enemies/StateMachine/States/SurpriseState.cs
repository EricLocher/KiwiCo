using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseState : EnemyState
{
    public SurpriseState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Surprise;

    public override void EnterState()
    {
        agent.surprise?.EnterSurprise();
    }

    public override void Update(float dt)
    {
        agent.surprise?.ActiveSurprise();
    }

    public override void ExitState()
    {
        agent.surprise?.ExitSurprise();

        stateMachine.ChangeState(EnemyStates.Chase);
    }
}
