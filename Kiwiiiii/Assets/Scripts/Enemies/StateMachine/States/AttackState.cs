using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public override void EnterState()
    {
        return;
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        return;
    }

}
