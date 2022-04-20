using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public SOEnemyStats enemyStats;

    private Animator animator;
    private AttackState attack;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<AttackState>();
    }

    private void Update()
    {
        if (attack.attacking)
        {
            animator.Play("Spin");
        }   
    }
}
