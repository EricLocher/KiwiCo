using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    BossPhase currentState = null;

    [HideInInspector] public Boss boss;
    [HideInInspector] public PatrolSpots moveSpots;

    Dictionary<BossPhases, BossPhase> states = new Dictionary<BossPhases, BossPhase>();

    public void Update()
    {
        currentState?.Update(Time.deltaTime);
    }

    public void ChangeState(BossPhases id)
    {
        currentState?.ExitPhase();
        currentState = states[id];
        currentState?.EnterPhase();
    }

    public void RegisterState(BossPhases stateID, BossPhase state)
    {
        states.Add(stateID, state);
    }
}
