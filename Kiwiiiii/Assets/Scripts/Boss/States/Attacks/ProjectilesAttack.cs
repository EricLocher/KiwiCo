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
    private float elapsedTime;

    public override void EnterState(BossPhase phase)
    {
        base.EnterState(phase);
        Debug.Log(currentPhase);
    }
    public override void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;
        if (elapsedTime > coolDownTime)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject _temp = Instantiate(projectile);
                float dX = 1 * Mathf.Cos(i * 2 * Mathf.PI / amount);
                float dY = 1 * Mathf.Sin(i * 2 * Mathf.PI / amount);
                _temp.transform.position = new Vector3(dX, dY, 0);
            }
            elapsedTime = 0;
        }
    }
    public override void ExitState()
    {
        currentPhase.RemoveSubState(this);
    }
}
