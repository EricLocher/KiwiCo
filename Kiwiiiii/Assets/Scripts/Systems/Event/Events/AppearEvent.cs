using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearEvent : GameEvent
{
    [SerializeField] public List<GameObject> objects;

    public override void Init()
    {
        foreach (GameObject obj in objects) {
            obj.SetActive(false);
        }
    }

    public override void StartEvent(EventZone zone)
    {

        this.zone = zone;
        foreach (GameObject obj in objects) {
            obj.SetActive(true);
        }
    }

    public override void UpdateEvent()
    {
        CompletedEvent();
    }

    public override void CompletedEvent()
    {
        zone.NextEvent();
    }

    public override string ToString()
    {
        if (eventName == "") { return "(AppearEvent)"; }
        return $"{eventName} (AppearEvent)";
    }
}
