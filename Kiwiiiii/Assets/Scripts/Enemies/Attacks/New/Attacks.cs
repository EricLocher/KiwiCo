using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    Transform target;
    [HideInInspector] public List<AttackCone> attacks = new List<AttackCone>();
    [SerializeField] Enemy agent;
    int randomIndex;
    public bool DealDamage = false;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Character").transform;
    }

    void Attack()
    {
        if (agent.stateMachine.activeState == EnemyStates.Attack) { return; }

        randomIndex = Random.Range(0, attacks.Count);

        agent.stateMachine.ChangeState(EnemyStates.Attack);

        agent.animator.SetTrigger(attacks[randomIndex].triggerName.ToString());
    }

    void Fire()
    {
        var animPlaying = attacks[randomIndex].triggerName.ToString();
        if (animPlaying == "shoot")
        {
            agent.animator.ResetTrigger("heal");
            if (agent.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == attacks[randomIndex].triggerName.ToString())
                GetComponent<FireProjectile>().CallCoroutine(agent.animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }

    void Heal()
    {
        var animPlaying = attacks[randomIndex].triggerName.ToString();
        if (animPlaying == "heal")
        {
            agent.animator.ResetTrigger("shoot");
            if (agent.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == attacks[randomIndex].triggerName.ToString())
                GetComponent<FireProjectile>().CallCoroutine(agent.animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }

    private void Update()
    {
        foreach (AttackCone attack in attacks)
        {
            if (attack.TargetInCone(target))
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                Attack();
                Fire();
                Heal();
                if (DealDamage)
                    EndOfAttack();
            }
            else if (!attack.TargetInCone(target) && agent.stateMachine.activeState == EnemyStates.Attack)
            {
                WaitForAttack();
            }
        }
    }

    public void EndOfAttack()
    {
        foreach (AttackCone attack in attacks)
        {
            target.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().TakeDamage(attack.damage);
        }
    }

    private async void WaitForAttack()
    {
        await Task.Delay((int)(agent.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * 1000f));
        ExitAttack();
    }

    public void ExitAttack()
    {
        agent.stateMachine.ChangeState(EnemyStates.Chase);
        agent.animator.ResetTrigger(attacks[randomIndex].triggerName.ToString());
    }


    public void AddAttack(AttackCone attack)
    {
        attacks.Add(attack);
    }

    public void RemoveAttack(AttackCone attack)
    {
        attacks.Remove(attack);
    }

}
