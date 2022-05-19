using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[Serializable, RequireComponent(typeof(NavMeshAgent), typeof(FOV), typeof(PatrolSpots))]
public class Enemy : Character
{
    public SOEnemyStats stats { get { return (SOEnemyStats)characterStats; } }

    [SerializeField] public Transform target;
    [SerializeField] public VisualEffect AppearEffect;
    [SerializeField] public Animator animator;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public FOV fov;
    [SerializeField] DamagePopup damagePopup;
    [SerializeField] float blinkIntensity, blinkDuration;
    float blinkTimer;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    public EnemyStateMachine stateMachine;

    protected override void Init()
    {
        target = GameObject.FindGameObjectWithTag("Character").transform;

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
        if (!navMeshAgent.isOnNavMesh) { Debug.LogError("Not on navmesh", this); return; }
        navMeshAgent.SetDestination(pos);
    }

    private void OnEnable()
    {
        AppearEffect.Play();
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1f;
        skinnedMeshRenderer.material.color = Color.white * intensity;

        stateMachine.Update();
    }

    public override void TakeDamage(float value)
    {
        characterStats.health -= value;
        var randomPos = UnityEngine.Random.Range(-2f, 2f);
        var collPos = transform.position;
        DamagePopup popup = Instantiate(damagePopup, new Vector3(collPos.x + randomPos, collPos.y + 2, collPos.z + randomPos), Quaternion.identity, TempHolder.transform);
        popup.PopupDamage((int)value);

        blinkTimer = blinkDuration;
        animator.SetTrigger("knockback");
        StartCoroutine(ResetKnockback(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length));
        AudioManager.instance.PlayOnceLocal("EnemyTakeDamage", gameObject);
        if (characterStats.health <= 0) { OnDeath(); }
    }

    IEnumerator ResetKnockback(float time)
    {
        yield return new WaitForSeconds(time);
        animator.ResetTrigger("knockback");
    }

    protected override void OnDeath()
    {
        //play death vfx on obj
        GameObject obj = new GameObject();
        obj.name = "DEATH VFX+AUDIO";
        obj.transform.position = transform.position;
        AudioManager.instance.PlayOnceLocal("EnemyDie", obj);
        Destroy(obj, 1f);

        stateMachine.ChangeState(EnemyStates.Death);
    }
}


