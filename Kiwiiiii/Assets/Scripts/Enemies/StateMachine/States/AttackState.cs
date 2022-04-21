using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public delegate void EnterAttack();
    public static event EnterAttack enterAttack;

    public delegate void ActiveAttack();
    public static event ActiveAttack activeAttack;

    public delegate void ExitAttack();
    public static event ExitAttack exitAttack;

    public override void EnterState()
    {
        enterAttack?.Invoke();
    }

    public override void Update()
    {
        activeAttack?.Invoke();

        if (!agent.fov.TargetInView(agent.target)) { stateMachine.ChangeState(EnemyStates.Idle); }

        if (Vector3.Distance(agent.transform.position, agent.target.position) < agent.fov.outerRadius)
        {
            stateMachine.ChangeState(EnemyStates.Chase);
        }
    }

    public override void ExitState()
    {
        exitAttack?.Invoke();
    }
}
