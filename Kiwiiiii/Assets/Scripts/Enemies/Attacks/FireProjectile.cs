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
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            GetComponent<Attacks>().isHealing = true;
            GetComponent<Animator>().ResetTrigger("shoot");
            GetComponent<Animator>().SetTrigger("heal");
            timer = 0;
        }

        if (GetComponent<Attacks>().isHealing)
        {
            StartCoroutine(Heal(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length));
        }
    }

    public void CallCoroutine(float time)
    {
        if (canShoot)
            StartCoroutine(InstantiateBall(time));
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
        Enemy healTarget = CheckRadius();

        if (healTarget == null) { ResetHeal(); yield return null; }

        Debug.Log(healTarget.stats.health);

        yield return new WaitForSeconds(time);

        healTarget.stats.health = 100;

        Debug.Log(healTarget.stats.health);

        ResetHeal();
    }

    void ResetHeal()
    {
        GetComponent<Attacks>().isHealing = false;
        GetComponent<Animator>().ResetTrigger("heal");
        GetComponent<Animator>().SetTrigger("shoot");
    }

    Enemy CheckRadius()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, 20f))
        {
            if(col.gameObject.tag != "Enemy") { return null; }
            var enemyComp = col.gameObject.GetComponent<Enemy>();
            if (enemyComp.stats.health == enemyComp.stats.maxHealth) { return null; }
            enemyList.Add(enemyComp);
            Enemy lowestHealth = enemyList[0];
            for (int i = 1; i < enemyList.Count; i++)
            {
                if (enemyList[i].stats.health < lowestHealth.stats.health)
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
