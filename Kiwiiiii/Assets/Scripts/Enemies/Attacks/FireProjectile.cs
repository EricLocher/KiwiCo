using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] Transform weapon;
    [SerializeField] VisualEffectAsset healVFX;
    List<Enemy> enemyList = new List<Enemy>();
    bool canShoot = true;
    float timer = 0;

    GameObject healObject;

    void Start()
    {
        healObject = new GameObject("Heal Effect");
        VisualEffect vfx = healObject.AddComponent<VisualEffect>();
        vfx.transform.parent = TempHolder.transform;
        vfx.visualEffectAsset = healVFX;
        vfx.Stop();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10) {
            GetComponent<Attacks>().isHealing = true;
            GetComponent<Animator>().ResetTrigger("shoot");
            GetComponent<Animator>().SetTrigger("heal");
            timer = 0;
        }

        if (GetComponent<Attacks>().isHealing) {
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
        GameObject sphereHolder = Instantiate(sphere, weapon.transform.position, weapon.transform.rotation);
        StartCoroutine(Reload(time));
        AudioManager.instance.PlayLocal("EnemyFireBurn", sphereHolder);
        AudioManager.instance.PlayOnceLocal("EnemyFire", gameObject);
    }

    IEnumerator Heal(float time)
    {
        Enemy healTarget = CheckRadius();

        yield return new WaitForSeconds(time);

        if (healTarget == null) { yield return null; }
        else {
            healObject.transform.position = healTarget.transform.position;
            healObject.GetComponent<VisualEffect>().Play();

            healTarget.stats.health = 100;
            GetComponent<Attacks>().isHealing = false;
            GetComponent<Animator>().ResetTrigger("heal");
            if (healTarget.stateMachine.activeState == EnemyStates.Attack) {
                GetComponent<Animator>().SetTrigger("shoot");
                yield return null;
            }

            GetComponent<Animator>().ResetTrigger("shoot");
        }
    }

    Enemy CheckRadius()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, 20f)) {
            if (col.gameObject.tag != "Enemy") { continue; }
            var enemyComp = col.gameObject.GetComponent<Enemy>();
            if(enemyComp == null) { continue; }
            if (enemyComp.stats.health == enemyComp.stats.maxHealth) { continue; }
            enemyList.Add(enemyComp);
            Enemy lowestHealth = enemyList[0];
            for (int i = 1; i < enemyList.Count; i++) {
                if (enemyList[i].stats.health < lowestHealth.stats.health) {
                    lowestHealth = enemyList[i];
                }
            }
            return lowestHealth;
        }

        return GetComponent<Enemy>();
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
