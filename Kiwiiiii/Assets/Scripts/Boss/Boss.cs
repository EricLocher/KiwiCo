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

    public List<Enemy> spawnedEnemies = new List<Enemy>();

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
        stateMachine.RegisterState(BossPhases.Death, new BossDeathState(this, stateMachine, phaseStats[0]));
        stateMachine.boss = this;
        stateMachine.ChangeState(BossPhases.Init);
        //healthBar.stats = stats;
    }

    void Update()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++) {
            if(spawnedEnemies[i] == null) {
                spawnedEnemies.RemoveAt(i);
                i--;
            }
        }


        stateMachine.Update();
    }

    public override void TakeDamage(float value)
    {
        stats.health -= value;

        ui.UpdateHealthBar(stats.health);
        //healthBar.stats.health = value;

        if (stats.health <= 0) { OnDeath(); }
        //stateMachine.ChangeState(BossPhases.Phase2);
        if (stats.health <= stats.maxHealth * stats.phase2Health && stateMachine.currentPhase == BossPhases.Phase1)
        {
            stateMachine.ChangeState(BossPhases.Phase2);
        }
        else if (stats.health <= stats.maxHealth * stats.phase3Health && stateMachine.currentPhase == BossPhases.Phase2)
        {
            stateMachine.ChangeState(BossPhases.Phase3);
        }

        AudioManager.instance.PlayOnce("BossDamage");

        ui.OnHit();
    }

    protected override void OnDeath()
    {
        AudioManager.instance.PlayOnce("BossDeath");

        for (int i = 0; i < spawnedEnemies.Count; i++) {
            if(spawnedEnemies[i] == null) { continue; }
            spawnedEnemies[i].TakeDamage(1000);
        }
        stateMachine.ChangeState(BossPhases.Death);

        Destroy(gameObject, 0.4f);
    }

    public void UpdateAttackStats()
    {

    }
}