using System;
using System.Collections;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] Transform weapon;
    bool canShoot = true;

    public void CallCoroutine(float time)
    {
        if(canShoot)
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

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
