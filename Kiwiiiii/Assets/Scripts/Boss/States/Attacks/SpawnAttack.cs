using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "BossSpawnAttack", menuName = "Utilities/BossAttacks/BossSpawnAttack")]
public class SpawnAttack : BossAttack
{

    [SerializeField] List<EnemyToSpawn> enemyList;
    [SerializeField] int amountOfEnemies = 5; 

    public override void EnterState(BossPhase phase)
    {
        base.EnterState(phase);
        AudioManager.instance.PlayOnce("BossSpawn");


        for (int i = 0; i < amountOfEnemies; i++) {
            Vector3 spawnPos = spawnSpots.GetNewSpot();
            if(boss.spawnedEnemies.Count >= 15) {
                continue;
            }
            Enemy enemy = Instantiate(enemyList[UnityEngine.Random.Range(0, enemyList.Count)].enemy, Vector3.zero, Quaternion.identity).GetComponent<Enemy>();
            enemy.GetComponent<NavMeshAgent>().Warp(spawnPos);

            boss.spawnedEnemies.Add(enemy);
        }
        ExitState();
    }
    public override void ExitState()
    {
        if(currentPhase != null)
            currentPhase.RemoveSubState(this);
    }
}

[Serializable]
class EnemyToSpawn
{
    public Enemy enemy;
    public float likelyhood;
}