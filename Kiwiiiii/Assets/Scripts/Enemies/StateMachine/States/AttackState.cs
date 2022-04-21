using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public delegate void TestAttack();
    public static event TestAttack testAttack;

    private Animator animator;

    public override void EnterState()
    {
        //animator = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();
        //animator.SetBool("attacking", true);
        return;
    }

    public override void Update()
    {
        testAttack?.Invoke();

        if (!agent.fov.TargetInView(agent.target)) { stateMachine.ChangeState(EnemyStates.Idle); }

        if (Vector3.Distance(agent.transform.position, agent.target.position) < agent.fov.outerRadius)
        {
            stateMachine.ChangeState(EnemyStates.Chase);
            return;
        }
    }

    public override void ExitState()
    {
        //animator = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();
        //animator.SetBool("attacking", false);
        return;
    }
}
