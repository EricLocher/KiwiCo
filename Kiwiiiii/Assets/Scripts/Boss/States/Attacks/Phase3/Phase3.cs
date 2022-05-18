using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3 : BossPhase
{
    public Phase3(Boss agent, BossStateMachine stateMachine, SOPhaseStats stats) : base(agent, stateMachine, stats) { }
    private int attackIndex = 0;
    private List<ComboAttacks> comboAttacks = new List<ComboAttacks>();

    public override BossPhases GetId() => BossPhases.Phase1;
    public override void EnterPhase()
    {
        comboAttacks.Add(new ComboAttacks(new List<BossAttack>()
        {
            stats.attackList[0], stats.attackList[1]
        }, this));
        comboAttacks.Add(new ComboAttacks(new List<BossAttack>()
        {
            stats.attackList[1], stats.attackList[2]
        }, this));
        comboAttacks.Add(new ComboAttacks(new List<BossAttack>()
        {
            stats.attackList[0], stats.attackList[2]
        }, this));
        comboAttacks.Add(new ComboAttacks(new List<BossAttack>()
        {
            stats.attackList[3]
        }, this));
        //comboAttacks.Add(new ComboAttacks(new List<BossAttack>()
        //{
        //    stats.attackList[0], stats.attackList[1]
        //}));
    }
    public override void Update(float dt = 0)
    {
        if(comboAttacks[attackIndex].isDone)
        {
            attackIndex++;
            if(attackIndex >= comboAttacks.Count)
            {
                attackIndex = 0;
            }
            comboAttacks[attackIndex].EnterAttack();
        }
        comboAttacks[attackIndex].Update(dt);
    }
    public override void ExitPhase()
    {

    }
}

class ComboAttacks
{
    private BossPhase phase;
    private List<BossAttack> attackList;
    public bool isDone;
    private float attackTimer;
    private float cooldown = 3;
    public ComboAttacks(List<BossAttack> attacks, BossPhase phase)
    {
        attackList = attacks;
        this.phase = phase;
    }

    public void EnterAttack()
    {
        isDone = false;
        cooldown = 10;
        for (int i = 0; i < attackList.Count; i++)
        {
            attackList[i].EnterState(phase);
            if (attackList[i].stateTime > attackTimer)
            {
                attackTimer = attackList[i].stateTime;
            }
        }
    }

    public void Update(float dt)
    {
        attackTimer -= dt;
        if (attackTimer <= 0)
        {
            cooldown -= dt;
            if(cooldown <= 0)
            {
                isDone = true;
            }
            foreach (var attacks in attackList)
            {
                attacks.ExitState();
            }
            return;
        }
        for (int i = 0; i < attackList.Count; i++)
        {
            Debug.Log(i);
            attackList[i].Update();
        }
    }
}