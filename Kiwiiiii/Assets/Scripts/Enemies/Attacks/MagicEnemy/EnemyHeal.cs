using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : EnemyAttack
{
    [SerializeField] HealRadius healRadius;

    public ParticleSystem healingUp;
    public ParticleSystem healingDown;

    public float healAmt;

    private Enemy thisEnemy;
    private Enemy chosenEnemy;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        thisEnemy = GetComponent<Enemy>();
    }

    public override void EnterAttack()
    {
        StartCoroutine(Heal());
    }

    public override void ActiveAttack()
    {
        return;
    }

    public override void ExitAttack()
    {
        return;
    }

    IEnumerator Heal()
    {
        chosenEnemy = healRadius.enemiesInRadius[0];
        animator.SetTrigger("heal");

        for (int i = 1; i < healRadius.enemiesInRadius.Count; i++)
        {
            if(healRadius.enemiesInRadius[i].stats.health < chosenEnemy.stats.health)
            {
                chosenEnemy = healRadius.enemiesInRadius[i];
            }
        }

        Vector3 upHealPosition = new Vector3(thisEnemy.transform.position.x, thisEnemy.transform.position.y + 3, thisEnemy.transform.position.z);

        ParticleSystem healingUpwards = Instantiate(healingUp, upHealPosition, healingUp.transform.rotation);
        chosenEnemy.Heal(healAmt);

        yield return new WaitForSeconds(2);

        Vector3 downHealPosition = new Vector3(chosenEnemy.transform.position.x, chosenEnemy.transform.position.y + 15, chosenEnemy.transform.position.z);

        ParticleSystem healingDownwards = Instantiate(healingDown, downHealPosition, healingDown.transform.rotation);
        animator.ResetTrigger("heal");

        thisEnemy.stateMachine.ChangeState(EnemyStates.Chase);
    }

}
