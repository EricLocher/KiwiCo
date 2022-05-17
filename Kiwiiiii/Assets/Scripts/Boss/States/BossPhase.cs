using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPhase 
{
    public Boss agent;
    public BossStateMachine stateMachine;
    public List<BossAttack> currentAttack = new List<BossAttack>();

    protected List<BossAttack> attacks;

    public BossPhase(Boss agent, BossStateMachine stateMachine, List<BossAttack> attacks)
    {
        this.agent = agent;
        this.stateMachine = stateMachine;
        this.attacks = attacks;
    }

    public abstract BossPhases GetId();
    public abstract void EnterPhase();
    public virtual void Update(float dt = 0)
    {
        foreach (BossAttack attack in currentAttack) {
            attack?.Update();
        }
    }

    public abstract void ExitPhase();

    public virtual void NextSubState(int index)
    {
        currentAttack.Add(attacks[index]);
        attacks[index].EnterState(this);
    }

    public virtual void RemoveSubState(BossAttack state)
    {
        if(currentAttack.Contains(state))
            currentAttack.Remove(state);
    }
}

public enum BossPhases
{
    Init,
    Phase1,
    Phase2,
    Phase3,
    Death
}