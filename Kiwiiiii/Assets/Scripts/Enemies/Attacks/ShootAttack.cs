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
        StartCoroutine(ShootBall());
    }
    public override void ActiveAttack()
    {
        sphereRB?.AddForce(direction *+ force);
    }

    public override void ExitAttack()
    {
        animator.SetBool("shooting", false);
    }

    IEnumerator ShootBall()
    {
        animator.SetBool("shooting", true);
        direction = (gun.transform.position - playerTransform.position).normalized;

        GameObject newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation);
        sphereRB = newSphere.GetComponent<Rigidbody>();

        yield return new WaitForSeconds(2);
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }
}
