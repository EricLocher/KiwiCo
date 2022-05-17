using System.Collections;
using UnityEngine;

public class SpawnState : BossState
{
    public SpawnState(Boss agent, BossStateMachine stateMachine) : base(agent, stateMachine) { }
    public override BossStates GetId() => BossStates.Spawning;

    int amountOfEnemiesToSpawn = 2;
    float elapsedTime = 0;
    float totalTime = 0;

    public override void EnterState()
    {
        elapsedTime = 0;
        Debug.Log(GetId());
    }

    public override void Update(float dt = 0)
    {
        elapsedTime += dt;
        totalTime += dt;
        if(elapsedTime > 5) {
            elapsedTime = 0;
            SpawnEnemies();
        }

        if(totalTime > 20) {
            stateMachine.ChangeState(BossStates.Shield);
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < amountOfEnemiesToSpawn; i++) {
            Vector3 pos = agent.spawnAreas.GetNewSpot();

            int index = Random.Range(0, agent.enemies.Count);
            Object.Instantiate(agent.enemies[index], pos, Quaternion.identity);
        }
    }

    public override void ExitState()
    {

    }

}

