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
    public abstract void Update(float dt = 0);
    public abstract void ExitState();

}

public enum EnemyStates
{
    Init,
    Idle,
    Surprise,
    Chase,
    Attack,
    Death
}