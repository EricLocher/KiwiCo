using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseState : EnemyState
{
    public SurpriseState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Surprise;

    public override void EnterState()
    {
        Debug.Log("Enter Surprise");
        agent.surprise?.EnterSurprise();
    }

    public override void Update(float dt)
    {
        //If no specific surprise behaviour (animation) is connected to enemy, go directly to chase state
        if (!agent.surprise)
        {
            stateMachine.ChangeState(EnemyStates.Chase);
        }
        //Otherwise wait 2 seconds (for animation to play) and then go to chase (method for waiting incorporated in specific surprise script)
        else
        {
            agent.surprise.ActiveSurprise();
            stateMachine.ChangeState(EnemyStates.Chase);
        }
    }

    public override void ExitState()
    {
        agent.surprise?.ExitSurprise();

        Debug.Log("Change to chase");
    }
}
