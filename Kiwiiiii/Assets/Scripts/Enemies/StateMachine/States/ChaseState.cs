using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Chase;

    public override void EnterState()
    {
        agent.chase?.EnterChase();
    }

    public override void Update(float dt)
    {
        agent.chase?.ActiveChase();

        agent.transform.LookAt(new Vector3(agent.target.position.x, agent.transform.position.y, agent.target.position.z));

        if (!agent.fov.TargetInView(agent.target)) { stateMachine.ChangeState(EnemyStates.Idle); }

        if (Vector3.Distance(agent.transform.position, agent.target.position) < agent.fov.innerRadius)
        {
            stateMachine.ChangeState(EnemyStates.Attack);
            return;
        }

        //Otherwise chase target.
        agent.SetDestination(agent.target.position);
    }

    public override void ExitState()
    {
        agent.chase?.ExitChase();
    }

}
