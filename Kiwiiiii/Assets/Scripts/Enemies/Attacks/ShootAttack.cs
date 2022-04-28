using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : MonoBehaviour
{
    [SerializeField]
    Enemy enemy;

    [Range(0.0f, 1.0f)]
    public float attackPropability;

    [Range(0.0f, 1.0f)]
    public float hitAccuracy;

    public float damagePoints = 3;

    private Animator animator;
    private PlayerHealth playerHealth;

    private float random;

    private bool shoot;

    void Start()
    {
        random = Random.Range(0.0f, 1.0f);
        animator = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void EnterAttack()
    {
        StartCoroutine(Shoot());
    }

    void ActiveAttack()
    {
        animator.SetBool("shooting", shoot);
    }

    IEnumerator Shoot()
    {
        shoot = false;

        if (random > (1.0f - attackPropability))
        {
            shoot = true;
            bool isHit = random > 1.0f - hitAccuracy;

            if (isHit)
            {
                playerHealth.TakeDamage(damagePoints);
            }
        }

        yield return new WaitForSeconds(2);
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }

    private void OnEnable()
    {
        AttackState.enterAttack += EnterAttack;
        AttackState.activeAttack += ActiveAttack;
    }

    private void OnDisable()
    {
        AttackState.enterAttack -= EnterAttack;
        AttackState.activeAttack -= ActiveAttack;
    }
}
