using System.Collections.Generic;
using UnityEngine;

public class BossInitState : BossPhase
{
    public BossInitState(Boss agent, BossStateMachine stateMachine, SOPhaseStats stats) : base(agent, stateMachine, stats) { }
    public override BossPhases GetId() => BossPhases.Init;

    public override void EnterPhase()
    {
        Debug.Log(GetId());
    }

    public override void Update(float dt = 0)
    {
        //Waiting for player to get close enough
        float dist = Vector3.Distance(agent.transform.position, agent.target.position);
        if(dist < 10) {
            stateMachine.ChangeState(BossPhases.Phase1);
        }
    }

    public override void ExitPhase()
    {

    }

}

