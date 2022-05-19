public class AttackState : EnemyState
{
    public AttackState(Enemy agent, EnemyStateMachine stateMachine) : base(agent, stateMachine) { }
    public override EnemyStates GetId() => EnemyStates.Attack;

    public override void EnterState()
    {
        agent.navMeshAgent.SetDestination(agent.transform.position);
    }

}
