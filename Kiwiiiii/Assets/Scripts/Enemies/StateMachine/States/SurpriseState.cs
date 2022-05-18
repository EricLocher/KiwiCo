using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseState : EnemyState
{
    public SurpriseState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Surprise;

    public override void EnterState()
    {
        agent.animator.SetTrigger("alert");
    }

    public override void Update(float dt)
    {
        WaitForAnimation();
        stateMachine.ChangeState(EnemyStates.Chase);
    }

    public override void ExitState()
    {
        agent.animator.ResetTrigger("alert");
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(2);
    }
}
