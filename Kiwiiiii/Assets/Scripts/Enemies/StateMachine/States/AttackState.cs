using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public bool attacking;
    public override void EnterState()
    {
        return;
    }

    public override void Update()
    {
        attacking = true;
    }

    public override void ExitState()
    {
        attacking = false;
        return;
    }

}
