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
        currentPhase = phase;
        //Debug.Log(currentPhase);

        for (int i = 0; i < amountOfEnemies; i++) {
            EnemyToSpawn enemyToSpawn;
            Vector3 spawnPos = spawnSpots.GetNewSpot();
            float randomValue = UnityEngine.Random.value;
            List<EnemyToSpawn> spawnList = new List<EnemyToSpawn>();
            foreach (var item in enemyList)
            {
                if (randomValue <= item.likelyhood)
                {
                    spawnList.Add(item);
                }
            }
            if(spawnList.Count > 0)
            {
                enemyToSpawn = spawnList[UnityEngine.Random.Range(0,spawnList.Count)];
            }
            else
            {
                enemyToSpawn = spawnList[0];
            }
            Instantiate(enemyToSpawn.enemy, Vector3.zero, Quaternion.identity).GetComponent<NavMeshAgent>().Warp(spawnPos);
        }
        ExitState();
    }
    public override void ExitState()
    {
        Debug.Log(currentPhase);
        currentPhase.RemoveSubState(this);
    }
}

[Serializable]
class EnemyToSpawn
{
    public Enemy enemy;
    public float likelyhood;

}