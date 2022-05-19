using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Utilities/BossAttacks/ProjectileAttack")]
public class ProjectilesAttack : BossAttack
{
    [SerializeField] GameObject projectile;
    [SerializeField] int amount = 5;
    [SerializeField] float coolDownTime = 3;
    [SerializeField] float damage;
    [SerializeField] float speed;
    private int spawnPointIndex = 0;
    private float elapsedTime;

    public override void EnterState(BossPhase phase)
    {
        spawnPointIndex = 0;
        elapsedTime = 0;
        base.EnterState(phase);

    }
    public override void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;
        if (elapsedTime > coolDownTime)
        {
            for (int i = 0; i < amount; i++)
            {
                float offsetAngle = ((spawnPointIndex % 2) == 0)? 180:0;
                ProjectileBehavior _temp = Instantiate(projectile).GetComponent<ProjectileBehavior>();
                float dX = 1 * Mathf.Cos(offsetAngle + i * 2 * Mathf.PI / amount);
                float dY = 1 * Mathf.Sin(offsetAngle + i * 2 * Mathf.PI / amount);
                _temp.transform.position = boss.transform.position + new Vector3(dX, 0, dY);
                _temp.transform.LookAt(boss.transform.position);
                _temp.transform.parent = TempHolder.transform;
                _temp.damage = damage;
                _temp.speed = speed;

            }
            spawnPointIndex++;
            elapsedTime = 0;
        }
    }
    public override void ExitState()
    {
        currentPhase.RemoveSubState(this);
    }
}