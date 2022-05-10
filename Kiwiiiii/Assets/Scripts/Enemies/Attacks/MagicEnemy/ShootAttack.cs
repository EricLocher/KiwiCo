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

    private int wave = 5;

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
        yield return new WaitForSeconds(5);
        //animator.SetTrigger("shoot");

        for (int i = 0; i < wave; i++)
        {
            yield return new WaitForSeconds(0.5f);

            SphereDamage newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation).GetComponent<SphereDamage>();

            AudioManager.instance.PlayLocal("EnemyFireBurn", newSphere.gameObject);
            AudioManager.instance.PlayOnceLocal("EnemyFire", gameObject);

            newSphere.target = playerController;
        }

        currentCoroutine = null;
    }
}
