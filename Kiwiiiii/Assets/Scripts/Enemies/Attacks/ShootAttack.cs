using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : MonoBehaviour
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


    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void EnterAttack()
    {
        StartCoroutine(ShootBall());
    }

    void ExitAttack()
    {
        animator.SetBool("shooting", false);
    }

    IEnumerator ShootBall()
    {
        animator.SetBool("shooting", true);
        Vector3 direction = (gun.transform.position - playerTransform.position).normalized;

        GameObject newSphere = Instantiate(sphere, gun.transform.position, gun.transform.rotation);
        sphereRB = newSphere.GetComponent<Rigidbody>();

        sphereRB.AddForce(direction * force);

        yield return new WaitForSeconds(2);
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }

    private void OnEnable()
    {
        AttackState.enterAttack += EnterAttack;
        AttackState.exitAttack += ExitAttack;
    }

    private void OnDisable()
    {
        AttackState.enterAttack -= EnterAttack;
        AttackState.exitAttack -= ExitAttack;
    }
}
