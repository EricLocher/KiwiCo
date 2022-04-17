public abstract class EnemyState
{
    public Enemy agent;
    public EnemyStateMachine stateMachine;

    public EnemyState(Enemy agent, EnemyStateMachine stateMachine)
    {
        this.agent = agent;
        this.stateMachine = stateMachine;
    }

    public abstract EnemyStates GetId();
    public abstract void EnterState();
    public abstract void Update();
    public abstract void ExitState();

}

public enum EnemyStates
{
    Init,
    Idle,
    Chase,
    Attack
}