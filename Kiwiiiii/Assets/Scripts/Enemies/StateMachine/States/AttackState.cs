using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public override void EnterState()
    {
        //not working sometimes...
        Vector3 dir = (agent.target.position - agent.transform.position).normalized * 2f;

        agent.navMeshAgent.SetDestination(agent.transform.position + dir);
        agent.animator.SetBool("moving", false);
    }

}
