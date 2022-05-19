public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public override void EnterState()
    {
        //not working sometimes...
        agent.navMeshAgent.SetDestination(agent.transform.position);
        agent.animator.SetBool("moving", false);
    }

}
