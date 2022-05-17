using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "BossSpawnAttack", menuName = "Utilities/BossAttacks/BossSpawnAttack")]
public class SpawnAttack : BossAttack
{

    [SerializeField] List<Enemy> enemyList;
    [SerializeField] int amountOfEnemies = 5; 

    public override void EnterState(BossPhase phase)
    {
        currentPhase = phase;

        Debug.Log(currentPhase);

        for (int i = 0; i < amountOfEnemies; i++) {
            Vector3 spawnPos = spawnSpots.GetNewSpot();
            Instantiate(enemyList[(int)Random.Range(0, enemyList.Count)], Vector3.zero, Quaternion.identity).GetComponent<NavMeshAgent>().Warp(spawnPos);
        }

        ExitState();
    }

    public override void ExitState()
    {
        currentPhase.RemoveSubState(this);
    }
}
