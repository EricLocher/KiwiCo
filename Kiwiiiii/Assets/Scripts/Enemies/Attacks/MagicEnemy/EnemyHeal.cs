using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : EnemyAttack
{
    [SerializeField] HealRadius healRadius;

    public float healAmt;

    private Enemy chosenEnemy;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public override void EnterAttack()
    {
        //TODO: Instantiate healing particle effect
        chosenEnemy = healRadius.enemiesInRadius[0];

        for (int i = 1; i < healRadius.enemiesInRadius.Count; i++)
        {
            if(healRadius.enemiesInRadius[i].stats.health < chosenEnemy.stats.health)
            {
                chosenEnemy = healRadius.enemiesInRadius[i];
            }
        }

        chosenEnemy.Heal(healAmt);
    }

    public override void ActiveAttack()
    {
        return;
    }

    public override void ExitAttack()
    {
        return;
    }

}
