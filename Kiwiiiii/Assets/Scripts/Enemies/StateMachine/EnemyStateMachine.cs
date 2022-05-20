using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateMachine
{
    EnemyState currentState = null;

    public EnemyStates activeState { get { return currentState.GetId(); } }

    [HideInInspector]
    public Enemy enemy;

    [HideInInspector]
    public PatrolSpots moveSpots;
    EnemyStates enemyState = EnemyStates.Idle;

    Dictionary<EnemyStates, EnemyState> states = new Dictionary<EnemyStates, EnemyState>();

    public void Update()
    {
        Debug.Log(currentState.GetId());
        //"?" check if the variable is a null reference, if not we call the specified function.
        currentState?.Update(Time.deltaTime);
    }

    public void ChangeState(EnemyStates id)
    {
        currentState?.ExitState();
        currentState = states[id];
        currentState?.EnterState();
        enemyState = id;
    }

    public void RegisterState(EnemyStates stateID, EnemyState state)
    {
        states.Add(stateID, state);
    }

}
