using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BossAttack : ScriptableObject
{
    public float stateTime = 0;

    protected PatrolSpots spawnSpots;
    public BossPhase currentPhase;
    protected float timeElapsed = 0;

    protected Boss boss;

    public void Init(PatrolSpots spawnSpots, Boss boss)
    {
        this.spawnSpots = spawnSpots;
        this.boss = boss;
    }

    public virtual void EnterState(BossPhase phase)
    {
        timeElapsed = 0;
        currentPhase = phase;
    }

    public virtual void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > stateTime)
        {
            ExitState();
        }
    }

    public virtual void ExitState()
    {
        currentPhase.RemoveSubState(this);
    }
}