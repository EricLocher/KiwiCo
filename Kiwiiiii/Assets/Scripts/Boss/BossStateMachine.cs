using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    BossState currentState = null;

    [HideInInspector]
    public Boss boss;

    [HideInInspector]
    public PatrolSpots moveSpots;

    Dictionary<BossStates, BossState> states = new Dictionary<BossStates, BossState>();

    public void Update()
    {
        currentState?.Update(Time.deltaTime);
    }

    public void ChangeState(BossStates id)
    {
        currentState?.ExitState();
        currentState = states[id];
        currentState?.EnterState();
    }

    public void RegisterState(BossStates stateID, BossState state)
    {
        states.Add(stateID, state);
    }
}
