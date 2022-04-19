using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Chase;

    public override void EnterState()
    {
        return;
    }

    public override void Update()
    {
        if (!agent.fov.TargetInView(agent.target)) { stateMachine.ChangeState(EnemyStates.Idle); }

        if (Vector3.Distance(agent.transform.position, agent.target.position) < agent.fov.innerRadius) {
            stateMachine.ChangeState(EnemyStates.Attack);
        }

        //Otherwise chase target.
        agent.SetDestination(agent.target);
    }

    public override void ExitState()
    {
        return;
    }

}
