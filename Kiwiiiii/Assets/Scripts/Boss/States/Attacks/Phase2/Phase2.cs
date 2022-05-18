using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : BossPhase
{
    public Phase2(Boss agent, BossStateMachine stateMachine, SOPhaseStats stats) : base(agent, stateMachine, stats) { }
    private float attackCooldown;
    private int attackIndex = 0;

    public override BossPhases GetId() => BossPhases.Phase1;
    public override void EnterPhase()
    {
        NextSubState(attackIndex);
        attackCooldown = stats.attackList[attackIndex].stateTime;
        Debug.Log(currentAttack);
    }
    public override void Update(float dt = 0)
    {
        base.Update(dt);
        if (attackCooldown <= 0)
        {
            attackIndex++;
            if (attackIndex >= stats.attackList.Count)
            {
                attackIndex = 0;
            }
            NextSubState(attackIndex);
            attackCooldown = stats.attackList[attackIndex].stateTime;
        }
        attackCooldown -= dt;
    }

    public override void ExitPhase()
    {

    }
}