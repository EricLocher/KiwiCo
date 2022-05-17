using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : EnemyAttack
{
    [SerializeField] Enemy enemy;
    [SerializeField] CapsuleCollider swingCollider;

    [Range(0, 10)]
    public int secondsBetweenSwings = 3;

    private PlayerController player;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    public override void EnterAttack()
    {
        StartCoroutine(Attack());
        return;
    }

    public override void ActiveAttack()
    {
        return;
    }

    public override void ExitAttack()
    {
        return;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("swing");
        swingCollider.enabled = true;

        yield return new WaitForSecondsRealtime(secondsBetweenSwings);

        animator.ResetTrigger("swing");
        swingCollider.enabled = false;
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            player.TakeDamage(0.1f);
        }
    }
}
