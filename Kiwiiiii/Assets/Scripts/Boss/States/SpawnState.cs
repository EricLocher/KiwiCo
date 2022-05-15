using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss_States.SpawnState
{
    public class SpawnState : BossState
    {
        public SpawnState(Boss agent, BossStateMachine stateMachine) : base(agent, stateMachine) { }
        public override BossStates GetId() => BossStates.Spawning;

        public override void EnterState()
        {
            Debug.Log(GetId());
        }

        public override void Update(float dt = 0)
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

    }
}
