using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : BossPhase
{
    public Phase2(Boss agent, BossStateMachine stateMachine, List<BossAttack> attacks) : base(agent, stateMachine, attacks) { }
    private float attackCooldown;
    private int attackIndex = 0;

    public override BossPhases GetId() => BossPhases.Phase1;
    public override void EnterPhase()
    {
        NextSubState(attackIndex);
        attackCooldown = attacks[attackIndex].stateTime;
        Debug.Log(currentAttack);

        //Granska detta Eric:
        agent.UpdateAttackStats();

    }
    public override void Update(float dt = 0)
    {
        base.Update(dt);
        if (attackCooldown <= 0)
        {
            attackIndex++;
            if (attackIndex >= attacks.Count)
            {
                attackIndex = 0;
            }
            NextSubState(attackIndex);
            attackCooldown = attacks[attackIndex].stateTime;
        }
        attackCooldown -= dt;
    }

    public override void ExitPhase()
    {

    }
}