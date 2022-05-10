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
    public Transform gun;
    public Transform playerTransform;

    private Animator animator;

    Coroutine currentCoroutine;

    PlayerController playerController;

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
        //animator.SetTrigger("shoot");

        for (int i = 0; i < numberOfShotsInWave; i++)
        {
            yield return new WaitForSeconds(secondsBetweenEachShot);

            SphereDamage newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation).GetComponent<SphereDamage>();

            //AudioManager.instance.PlayLocal("EnemyFireBurn", newSphere.gameObject);
            //AudioManager.instance.PlayOnceLocal("EnemyFire", gameObject);

            newSphere.target = playerController;
        }

        yield return new WaitForSeconds(secondsBetweenWaves);

        currentCoroutine = null;
    }
}
