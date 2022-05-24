using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Quest.Dialogue;

public class SwordEvent : GameEvent
{
    public override void Init(){}

    public override void StartEvent(EventZone zone)
    {
        this.zone = zone;
    }

    public override void UpdateEvent()
    {
        if (Save.instance.aquiredSword) { CompletedEvent(); }
    }

    public override void CompletedEvent()
    {
        zone.NextEvent();
    }

    public override string ToString()
    {
        if (eventName == "") { return "(SwordEvent)"; }
        return $"{eventName} (SwordEvent)";
    }
}
