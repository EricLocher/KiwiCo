using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEvent : GameEvent
{
    public NPC npc;


    public override void StartEvent(EventZone zone)
    {
        this.zone = zone;
        npc.gameObject.SetActive(true);
    }

    public override void UpdateEvent()
    {
        CompletedEvent();
    }
    public override void CompletedEvent()
    {
        zone.NextEvent();
    }
}
