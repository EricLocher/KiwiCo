using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPhase 
{
    public Boss agent;
    public BossStateMachine stateMachine;
    public List<BossAttack> currentAttack = new List<BossAttack>();
    public SOPhaseStats stats;

    public BossPhase(Boss agent, BossStateMachine stateMachine, SOPhaseStats stats)
    {
        this.agent = agent;
        this.stateMachine = stateMachine;
        this.stats = stats;
    }

    public abstract BossPhases GetId();

    public virtual void Init()
    {
        stats = GameObject.Instantiate(stats);

        for(int i = 0; i < stats.attackList.Count; i++)
        {
            stats.attackList[i] = GameObject.Instantiate(stats.attackList[i]);
            stats.attackList[i].Init(agent.spawnAreas, agent);
        }
    }

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
        currentAttack.Add(stats.attackList[index]);
        stats.attackList[index].EnterState(this);
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