using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public Transform target;
    [HideInInspector] public List<AttackCone> attacks = new List<AttackCone>();
    [SerializeField] Enemy agent;
    int randomIndex;

    void Attack()
    {
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

    private void Update()
    {
        foreach (AttackCone attack in attacks)
        {
            if (attack.TargetInCone(target))
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                Attack();
                Fire();
            }
            else if (!attack.TargetInCone(target) && agent.stateMachine.activeState == EnemyStates.Attack)
            {
                WaitForAttack();
            }
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
