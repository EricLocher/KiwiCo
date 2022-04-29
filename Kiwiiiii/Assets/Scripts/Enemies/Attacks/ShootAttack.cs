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
    private Rigidbody sphereRB;
    private Transform playerTransform;

    private Vector3 direction;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void EnterAttack()
    {
        StartCoroutine(InstantiateBall());
    }

    public override void ActiveAttack()
    {
        return;
    }

    public override void ExitAttack()
    {
        animator.SetBool("shooting", false);
    }

    IEnumerator InstantiateBall()
    {
        animator.SetBool("shooting", true);
        direction = (playerTransform.position - gun.transform.position).normalized;

        SphereDamage newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation).GetComponent<SphereDamage>();
        newSphere.direction = direction;

        yield return new WaitForSeconds(2);
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }
}
