using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public List<Enemy> enemies;
    public BossHealthBar healthBar;
    [HideInInspector] public Transform target;
    [HideInInspector] public PatrolSpots spawnAreas;
    public SOBossStats stats { get { return (SOBossStats)characterStats; } }

    BossStateMachine stateMachine;

    protected override void Init()
    {
        target = GameObject.FindGameObjectWithTag("Character").transform;
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

    public override void DealDamage(float value)
    {
        print("Boss took damage: " + value);
        characterStats.health -= value;

        if (characterStats.health <= 0) { OnDeath(); }
    }

}
