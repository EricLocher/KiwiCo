using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] Transform weapon;
    List<Enemy> enemyList = new List<Enemy>();
    bool canShoot = true;
    public bool heal = false;

    public void CallCoroutine(float time)
    {
        if (canShoot)
            StartCoroutine(InstantiateBall(time));

        if (heal)
            StartCoroutine(Heal(time));
    }

    IEnumerator InstantiateBall(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        Instantiate(sphere, weapon.transform.position, weapon.transform.rotation).GetComponent<SphereDamage>();
        StartCoroutine(Reload(time));
        //AudioManager.instance.PlayLocal("EnemyFireBurn", sphere);
        //AudioManager.instance.PlayOnceLocal("EnemyFire", gameObject);
    }

    IEnumerator Heal(float time)
    {
        canShoot = false;
        heal = false;
        yield return new WaitForSeconds(time);
        Enemy healTarget = CheckRadius();
        if(healTarget != null)
        {
            healTarget.stats.health = 100;

            Debug.Log("healed");

            StartCoroutine(Reload(time));
        }
        else
        {
            canShoot = true;
        }
    }

    Enemy CheckRadius()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, 20f, 2))
        {
            if(col.gameObject.GetComponent<SOEnemyStats>().health == 100) { return null; }

            enemyList.Add(col.gameObject.GetComponent<Enemy>());
            Enemy lowestHealth = enemyList[0];
            for (int i = 1; i < enemyList.Count; i++)
            {
                if(enemyList[i].stats.health < lowestHealth.stats.health)
                {
                    lowestHealth = enemyList[i];
                }
            }
            return lowestHealth;
        }
        

        return null;
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
