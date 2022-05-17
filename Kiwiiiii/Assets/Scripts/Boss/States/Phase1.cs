using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : BossPhase
{
    public Phase1(Boss agent, BossStateMachine stateMachine, List<BossAttack> attacks) : base(agent, stateMachine, attacks) {}

    public override BossPhases GetId() => BossPhases.Phase1;
    public override void EnterPhase()
    {
        NextSubState(0);
        Debug.Log(currentAttack);
    }

    public override void Update(float dt = 0)
    {
        base.Update(dt);
    }

    public override void ExitPhase()
    {
        
    }

}
