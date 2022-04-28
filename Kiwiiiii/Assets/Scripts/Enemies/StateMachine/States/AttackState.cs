using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public override void EnterState()
    {
        agent.attack.EnterAttack();
    }

    public override void Update(float dt)
    {
        agent.attack.ActiveAttack();
    }

    public override void ExitState()
    {
        agent.attack.ExitAttack();
    }
}
