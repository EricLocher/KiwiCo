using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    private Animator animator;

    public override void EnterState()
    {
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();
        return;
    }

    public override void Update()
    {
        animator.Play("Spin");
        return;
    }

    public override void ExitState()
    {
        return;
    }

}
