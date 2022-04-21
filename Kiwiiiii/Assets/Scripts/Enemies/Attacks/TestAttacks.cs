using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttacks : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void EnterAttack()
    {
        animator.SetBool("attacking", true);
    }

    void ExitAttack()
    {
        animator.SetBool("attacking", false);
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
