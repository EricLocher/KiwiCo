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

    PlayerController playerController;

    Coroutine currentCoroutine;

    void Start()
    {
        currentCoroutine = null;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public override void EnterAttack()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(InstantiateBall());
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
        //animator.SetTrigger("idle");
    }

    IEnumerator InstantiateBall()
    {

        enemy.stateMachine.ChangeState(EnemyStates.Chase);
        yield return new WaitForSeconds(1);
        //animator.SetTrigger("shoot");

        SphereDamage newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation).GetComponent<SphereDamage>();
        newSphere.target = playerController;

        currentCoroutine = null;
    }
}
