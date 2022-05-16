using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState 
{
    public Boss agent;
    public BossStateMachine stateMachine;

    public BossState(Boss agent, BossStateMachine stateMachine)
    {
        this.agent = agent;
        this.stateMachine = stateMachine;
    }

    public abstract BossStates GetId();
    public abstract void EnterState();
    public abstract void Update(float dt = 0);
    public abstract void ExitState();

}

public enum BossStates
{
    Init,
    Shield,
    Spawning,
    Shooting,
    Death
}