using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    Transform target;
    public List<AttackCone> attacks = new List<AttackCone>();
    Enemy agent;
    int randomIndex;
    [HideInInspector] public bool DealDamage, isHealing = false;
    void Start()
    {
        agent = GetComponent<Enemy>();
        for (int i = 0; i < attacks.Count; i++)
        {
            attacks[i] = Instantiate(attacks[i]);
            attacks[i].origin = transform;
        }
        target = GameObject.FindGameObjectWithTag("Character").transform;
    }

    void Attack()
    {
        if (agent == null) { return; }
        if (agent.stateMachine.activeState == EnemyStates.Attack) { return; }
        randomIndex = Random.Range(0, attacks.Count);

        agent.stateMachine.ChangeState(EnemyStates.Attack);

        agent.animator.SetTrigger(attacks[randomIndex].triggerName.ToString());
    }

    void Fire()
    {
        if (attacks[randomIndex].triggerName.ToString() == "shoot")
        {
            if (agent.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == attacks[randomIndex].triggerName.ToString())
                GetComponent<FireProjectile>().CallCoroutine(agent.animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }

    void Update()
    {
        foreach (AttackCone attack in attacks)
        {
            if (attack.TargetInCone(target))
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                Attack();

                if (!isHealing)
                    Fire();

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
        if (agent == null) { return; }
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
