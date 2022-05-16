using UnityEngine;

public class ShieldState : BossState
{
    public ShieldState(Boss agent, BossStateMachine stateMachine) : base(agent, stateMachine) { }
    public override BossStates GetId() => BossStates.Shield;

    float test = 5;
    float elapsedTime = 0; 

    public override void EnterState()
    {
        elapsedTime = 0;
        Debug.Log(GetId());
    }

    public override void Update(float dt = 0)
    {
        elapsedTime += dt;
        if(elapsedTime > test) {
            stateMachine.ChangeState(BossStates.Spawning);
        }
    }

    public override void ExitState()
    {

    }

}

