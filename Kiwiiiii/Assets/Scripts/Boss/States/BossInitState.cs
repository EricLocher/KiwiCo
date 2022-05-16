using UnityEngine;

public class BossInitState : BossState
{
    public BossInitState(Boss agent, BossStateMachine stateMachine) : base(agent, stateMachine) { }
    public override BossStates GetId() => BossStates.Init;

    public override void EnterState()
    {
        Debug.Log(GetId());
    }

    public override void Update(float dt = 0)
    {
        //Waiting for player to get close enough
        float dist = Vector3.Distance(agent.transform.position, agent.target.position);
        if(dist < 10) { stateMachine.ChangeState(BossStates.Shield); }

    }

    public override void ExitState()
    {

    }

}

