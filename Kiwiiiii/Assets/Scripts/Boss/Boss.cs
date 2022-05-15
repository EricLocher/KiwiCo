using System.Collections.Generic;
using UnityEngine;


public class Boss : Character
{
    [SerializeField] List<Enemy> enemies;
    [HideInInspector] public Transform target;
    [HideInInspector] public PatrolSpots spawnAreas;
    public SOBossStats stats { get { return (SOBossStats)stats; } }

    BossStateMachine stateMachine;

    protected override void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        spawnAreas = GetComponent<PatrolSpots>();
        stateMachine.boss = this;
    }

    void Update()
    {
        stateMachine.Update();
    }
}
