using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateMachine
{
    EnemyState currentState = null;
    
    [HideInInspector]
    public Enemy enemy;

    Dictionary<EnemyStates, EnemyState> states = new Dictionary<EnemyStates, EnemyState>();

    public Transform[] moveSpots;

    public EnemyStates enemyState = EnemyStates.Idle;

    public void Update()
    {
        Debug.Log(moveSpots);
        //"?" check if the variable is a null reference, if not we call the specified function.
        currentState?.Update();
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
