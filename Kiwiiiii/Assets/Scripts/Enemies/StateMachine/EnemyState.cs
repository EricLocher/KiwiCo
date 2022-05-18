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
    public virtual void EnterState() { }
    public virtual void Update(float dt = 0) { }
    public virtual void ExitState() { }

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