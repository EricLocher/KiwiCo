using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class EnemyEvent : GameEvent
{
    [SerializeField] public List<Enemy> enemyList;

    public override void Init()
    {
        foreach (Enemy enemy in enemyList) {
            enemy.gameObject.SetActive(false);
        }
    }

    public override void StartEvent(EventZone zone)
    {
        this.zone = zone;
        foreach (Enemy enemy in enemyList) {
            enemy.gameObject.SetActive(true);
        }
        //invert collider normal
        //remove is trigger
    }

    public override void UpdateEvent()
    {
        //Checks if all enemies are dead, in that case complete the event.
        bool check = true;
        foreach (Enemy enemy in enemyList) {
            if (enemy != null) { check = false; break; }
        }

        if (check) { CompletedEvent(); }
    }

    public override void CompletedEvent()
    {
        zone.NextEvent();
    }

    public override string ToString()
    {
        if(eventName == "") { return "(EnemyEvent)"; }
        return $"{eventName} (EnemyEvent)";
    }
}
