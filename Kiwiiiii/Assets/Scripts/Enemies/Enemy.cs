using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[Serializable, RequireComponent(typeof(NavMeshAgent), typeof(FOV), typeof(PatrolSpots))]
public class Enemy : Character
{
    public SOEnemyStats stats { get { return (SOEnemyStats)stats; } }

    [SerializeField] public Transform target;
    [SerializeField] public VisualEffect AppearEffect;
    [SerializeField] public Animator animator;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public FOV fov;
    [SerializeField] DamagePopup damagePopup;
    [HideInInspector] public EnemyAttack attack;

    public EnemyIdle idle;
    public EnemyChase chase;
    public EnemySurprise surprise;
    public EnemyStateMachine stateMachine;

    public List<EnemyAttack> enemyAttacks;

    protected override void Init()
    {
        fov = GetComponent<FOV>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.moveSpots = GetComponent<PatrolSpots>();
        stateMachine.enemy = this;

        stateMachine.RegisterState(EnemyStates.Idle, new IdleState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Surprise, new SurpriseState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Chase, new ChaseState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Attack, new AttackState(this, stateMachine));
        stateMachine.RegisterState(EnemyStates.Death, new DeathState(this, stateMachine));

        stateMachine.ChangeState(EnemyStates.Idle);
    }

    public void SetDestination(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    private void OnEnable()
    {
        AppearEffect.Play();
    }

    private void Update()
    {
        if (navMeshAgent.hasPath)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        stateMachine.Update();
    }

    public override void DealDamage(float value)
    {
        CharacterStats.health -= value;
        Debug.Log("test");
        var randomPos = UnityEngine.Random.Range(-2f, 2f);
        var collPos = transform.position;
        DamagePopup popup = Instantiate(damagePopup, new Vector3(collPos.x + randomPos, collPos.y + 2, collPos.z + randomPos), Quaternion.identity, TempHolder.transform);
        popup.PopupDamage((int)value);
        StartCoroutine("ResetColor");
        if (CharacterStats.health <= 0) { OnDeath(); }
    }

    IEnumerator ResetColor()
    {
        var originalColor = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color;
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = originalColor;
    }

    protected override void OnDeath()
    {
        stateMachine.ChangeState(EnemyStates.Death);
    }
}


