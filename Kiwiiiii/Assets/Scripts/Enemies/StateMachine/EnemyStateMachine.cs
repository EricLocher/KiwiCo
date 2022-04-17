using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    EnemyState currentState = null;
    readonly Enemy enemy;

    Dictionary<EnemyStates, EnemyState> states = new Dictionary<EnemyStates, EnemyState>();

    public EnemyStateMachine(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Update()
    {
        //"?" check if the variable is a null reference, if not we call the specified function.
        currentState?.Update();
    }

    public void ChangeState(EnemyStates id)
    {
        currentState?.ExitState();
        currentState = states[id];
        currentState?.EnterState();
    }

    public void RegisterState(EnemyStates stateID, EnemyState state)
    {
        states.Add(stateID, state);
    }

}
