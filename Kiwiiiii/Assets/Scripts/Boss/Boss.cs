using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    //public BossHealthBar healthBar;
    BossUI ui;
    [HideInInspector] public Transform target;
    [HideInInspector] public PatrolSpots spawnAreas;
    public List<SOPhaseStats> phaseStats = new List<SOPhaseStats>();
    public SOBossStats stats { get { return (SOBossStats)characterStats; } }

    BossStateMachine stateMachine;

    private void Start()
    {
        ui = GetComponent<BossUI>();
    }

    protected override void Init()
    {
        target = GameObject.FindGameObjectWithTag("Character").transform;
        spawnAreas = GetComponent<PatrolSpots>();

        stateMachine = new BossStateMachine();

        for (int i = 0; i < phaseStats.Count; i++)
        {
            phaseStats[i] = Instantiate(phaseStats[i]);
        }

        stateMachine.RegisterState(BossPhases.Init, new BossInitState(this, stateMachine, phaseStats[0]));
        stateMachine.RegisterState(BossPhases.Phase1, new Phase1(this, stateMachine, phaseStats[0]));
        stateMachine.RegisterState(BossPhases.Phase2, new Phase2(this, stateMachine, phaseStats[1]));
        stateMachine.RegisterState(BossPhases.Phase3, new Phase3(this, stateMachine, phaseStats[2]));
        stateMachine.boss = this;
        stateMachine.ChangeState(BossPhases.Init);
        //healthBar.stats = stats;
    }

    void Update()
    {
        stateMachine.Update();
    }

    public override void TakeDamage(float value)
    {
        print("Boss took damage: " + value);
        stats.health -= value;

        ui.UpdateHealthBar(stats.health);
        //healthBar.stats.health = value;

        if (stats.health <= 0) { OnDeath(); }

        if (stats.health <= stats.maxHealth * stats.phase2Health && stats.health >= stats.maxHealth * stats.phase1Health)
        {
            stateMachine.ChangeState(BossPhases.Phase2);
        }
        else if (stats.health <= stats.maxHealth * stats.phase3Health)
        {
            stateMachine.ChangeState(BossPhases.Phase3);
        }
    }
    public void UpdateAttackStats()
    {

    }
}