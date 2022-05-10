using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAttack : EnemyAttack
{
    [SerializeField] Enemy enemy;

    [Range(0, 10)]
    public int secondsBetweenStabs = 3;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void EnterAttack()
    {
        return;
    }

    public override void ActiveAttack()
    {
        Attack();
    }

    public override void ExitAttack()
    {
        return;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("stab");
        //TODO: OnCollision damage
        yield return new WaitForSeconds(secondsBetweenStabs);

        animator.ResetTrigger("stab");
        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }
}
