using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossPhase
{
    public BossDeathState(Boss agent, BossStateMachine stateMachine, SOPhaseStats stats) : base(agent, stateMachine, stats) { }
    public override BossPhases GetId() => BossPhases.Death;

    public override void EnterPhase()
    {
    }

    public override void Update(float dt = 0)
    {
        
    }
}