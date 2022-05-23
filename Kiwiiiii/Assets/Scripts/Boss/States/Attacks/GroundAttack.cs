using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundAttack", menuName = "Utilities/BossAttacks/GroundAttack")]
public class GroundAttack : BossAttack
{
    [SerializeField] GroundSpikesBehavior prefab;
    [SerializeField] float damage = 1;
    [SerializeField] float speed = 1;
    [SerializeField] float amountOfAttacks = 1;
    [SerializeField] float timeBetweeenAttacks = 1;

    float timer = 0;
    float attackIndex = 0;

    public override void EnterState(BossPhase phase)
    {
        base.EnterState(phase);
        timer = 0;
        attackIndex = 0;
        stateTime = amountOfAttacks * (speed + timeBetweeenAttacks) + 2;
    }

    public override void Update()
    {
        base.Update();

        if(timer > (speed + timeBetweeenAttacks) && attackIndex < amountOfAttacks) {
            Attack();
            timer = 0;
            attackIndex++;
        }

        timer += Time.deltaTime;
    }

    public void Attack()
    {
        Vector3 from = boss.transform.forward;
        Vector3 to = (boss.target.transform.position - boss.transform.position).normalized;

        float angle = Vector3.SignedAngle(from, to, Vector3.up);

        float rot = 0;

        if (angle > 0 && angle < 90) { rot = 0; }
        else if (angle > 90 && angle < 180) { rot = 90; }
        else if (angle < 0 && angle > -90) { rot = 270; }
        else if (angle < -90 && angle > -180) { rot = 180; }

        GroundSpikesBehavior attack = Instantiate(prefab, boss.transform);
        attack.transform.rotation = Quaternion.Euler(0, rot, 0);
        attack.Attack(speed, damage, boss.target);
    }

    public override void ExitState()
    {
        currentPhase.RemoveSubState(this);
    }
}
