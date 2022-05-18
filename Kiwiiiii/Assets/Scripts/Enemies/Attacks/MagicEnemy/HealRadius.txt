using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealRadius : MonoBehaviour
{
    public List<Enemy> enemiesInRadius = new List<Enemy>();

    private Enemy thisEnemy;

    private void Start()
    {
        thisEnemy = GetComponentInParent<Enemy>();
        enemiesInRadius.Add(thisEnemy);
    }

    private void OnTriggerEnter(Collider col)
    {
        Enemy temp = col.GetComponent<Enemy>();

        if (temp == null) { return; }
        enemiesInRadius.Add(temp);
    }

    private void OnTriggerExit(Collider col)
    {
        Enemy temp = col.GetComponent<Enemy>();

        if (temp == null) { return; }

        if (enemiesInRadius.Contains(temp))
        {
            enemiesInRadius.Remove(temp);
        }
    }
}
