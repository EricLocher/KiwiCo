using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    private int chosenAttack;

    public override void EnterState()
    {
        chosenAttack = Random.Range(0, agent.enemyAttacks.Count);
        agent.attack = agent.enemyAttacks[chosenAttack];

        agent.attack?.EnterAttack();
    }

    public override void Update(float dt)
    {
        agent.attack?.ActiveAttack();
    }

    public override void ExitState()
    {
        agent.attack?.ExitAttack();
    }
}
