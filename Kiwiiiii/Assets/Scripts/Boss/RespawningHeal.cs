using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningHeal : MonoBehaviour
{
    [SerializeField] float RespawnTime = 10;
    [SerializeField] HealItem healPrefab;

    HealItem heal = null;
    bool respawning = true;
    float coolDown = 0;

    private void Update()
    {
        if(respawning && coolDown <= 0) {
            respawning = false;
            heal = Instantiate(healPrefab);
            heal.transform.position = transform.position + Vector3.up * 2.5f;
            heal.transform.parent = transform;
            heal.holder = this;
        }

        coolDown -= Time.deltaTime;
    }
    
    public void Respawn()
    {
        respawning = true;
        coolDown = Random.Range(RespawnTime, RespawnTime * 2);
    }
}
