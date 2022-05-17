using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public BossHealthBar healthBar;
    [HideInInspector] public Transform target;
    [HideInInspector] public PatrolSpots spawnAreas;
    [SerializeField] List<BossAttack> attacks;
    public SOBossStats stats { get { return (SOBossStats)characterStats; } }

    BossStateMachine stateMachine;

    protected override void Init()
    {
        target = GameObject.FindGameObjectWithTag("Character").transform;
        spawnAreas = GetComponent<PatrolSpots>();

        stateMachine = new BossStateMachine();

        foreach (BossAttack attack in attacks) {
            attack.Init(spawnAreas, this);
        }

        stateMachine.RegisterState(BossPhases.Init, new BossInitState(this, stateMachine, attacks));
        stateMachine.RegisterState(BossPhases.Phase1, new Phase1(this, stateMachine, attacks));
        stateMachine.boss = this;
        stateMachine.ChangeState(BossPhases.Init);

        healthBar.stats = stats;
    }

    void Update()
    {
        stateMachine.Update();
    }

    public override void TakeDamage(float value)
    {
        print("Boss took damage: " + value);
        characterStats.health -= value;

        if (characterStats.health <= 0) { OnDeath(); }
    }

}
