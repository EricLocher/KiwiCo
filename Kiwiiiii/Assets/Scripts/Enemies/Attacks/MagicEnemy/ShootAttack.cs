using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : EnemyAttack
{
    [SerializeField]
    Enemy enemy;

    [Range(0, 50)]
    public float force = 5;

    [Range(0, 50)]
    public float damagePoints = 3;

    [Range(0, 10)]
    public float secondsBetweenEachShot = 0.5f;

    [Range(0, 30)]
    public float secondsBetweenWaves = 3;


    [Range(0, 50)]
    public int numberOfShotsInWave = 5;

    public GameObject sphere;
    public Transform weapon;

    Coroutine currentCoroutine;
    PlayerController playerController;
    Animator animator;

    void Start()
    {
        currentCoroutine = null;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
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
        currentCoroutine = null;
    }

    IEnumerator InstantiateBall()
    {
        animator.SetTrigger("attack");

        for (int i = 0; i < numberOfShotsInWave; i++)
        {
            yield return new WaitForSeconds(secondsBetweenEachShot);

            SphereDamage newSphere = Instantiate(sphere, weapon.transform.position, weapon.transform.rotation).GetComponent<SphereDamage>();

            //AudioManager.instance.PlayLocal("EnemyFireBurn", newSphere.gameObject);
            //AudioManager.instance.PlayOnceLocal("EnemyFire", gameObject);

            newSphere.target = playerController;
        }

        yield return new WaitForSeconds(secondsBetweenWaves);
        animator.ResetTrigger("attack");

        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }
}
