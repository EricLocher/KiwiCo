using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyEvent : GameEvent
{
    [SerializeField] public List<Enemy> enemyList;

    public override void StartEvent(EventZone zone)
    {
        this.zone = zone;
        foreach (Enemy enemy in enemyList) {
            enemy.gameObject.SetActive(true);
        }
    }

    public override void UpdateEvent()
    {
        //Checks if all enemies are dead, in that case complete the event.
        bool check = true;
        foreach (Enemy enemy in enemyList) {
            if(enemy != null) { check = false; break; }
        }

        if (check) { CompletedEvent(); }
    }

    public override void CompletedEvent()
    {
        zone.NextEvent();
    }
}
