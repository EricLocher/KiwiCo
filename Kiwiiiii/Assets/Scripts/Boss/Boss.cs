using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public List<Enemy> enemies;
    public BossHealthBar healthBar;
    [HideInInspector] public Transform target;
    [HideInInspector] public PatrolSpots spawnAreas;
    public SOBossStats stats { get { return stats; } }

    BossStateMachine stateMachine;

    protected override void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        spawnAreas = GetComponent<PatrolSpots>();

        stateMachine = new BossStateMachine();

        stateMachine.RegisterState(BossStates.Init, new BossInitState(this, stateMachine));
        stateMachine.RegisterState(BossStates.Shield, new ShieldState(this, stateMachine));
        stateMachine.RegisterState(BossStates.Spawning, new SpawnState(this, stateMachine));
        stateMachine.boss = this;
        stateMachine.ChangeState(BossStates.Init);

        healthBar.stats = stats;
    }

    void Update()
    {
        stateMachine.Update();
    }
}
