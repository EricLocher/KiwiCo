using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public Transform target;
    [HideInInspector] public List<AttackCone> attacks = new List<AttackCone>();
    [SerializeField] Enemy agent;
    int randomIndex;

    void Attack()
    {
        randomIndex = Random.Range(0, attacks.Count);

        agent.stateMachine.ChangeState(EnemyStates.Attack);
        agent.animator.SetTrigger(attacks[randomIndex].triggerName.ToString());
    }

    private void Update()
    {
        foreach (AttackCone attack in attacks) {
            if (attack.TargetInCone(target)) {
                Attack();
            }
        }
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
