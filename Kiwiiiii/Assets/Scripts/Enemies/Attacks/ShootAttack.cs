using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : EnemyAttack
{
    [SerializeField]
    Enemy enemy;

    public float force = 5;
    public float damagePoints = 3;

    public GameObject sphere;
    public Transform gun;

    private Animator animator;

    Coroutine currentCoroutine;

    void Start()
    {
        currentCoroutine = null;
        animator = GetComponent<Animator>();
    }

    public override void EnterAttack()
    {
        if (currentCoroutine == null)
        {
            Vector3 direction = (enemy.target.position - gun.transform.position).normalized;
            currentCoroutine = StartCoroutine(InstantiateBall(direction));
        }
        else
        {
            enemy.stateMachine.ChangeState(EnemyStates.Chase);
        }
    }

    public override void ActiveAttack()
    {
        return;
    }

    public override void ExitAttack()
    {
        animator.SetTrigger("idle");
    }

    IEnumerator InstantiateBall(Vector3 direction)
    {
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
        yield return new WaitForSeconds(2);
        animator.SetTrigger("shoot");

        SphereDamage newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation).GetComponent<SphereDamage>();
        newSphere.direction = direction;

        currentCoroutine = null;
    }
}
