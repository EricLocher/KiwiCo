using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class NPCEvent : GameEvent
{
    [SerializeField] public List<NPC> npcList;

    public override void Init()
    {
        foreach (NPC npc in npcList)
        {
            npc.gameObject.SetActive(false);
        }
    }

    public override void StartEvent(EventZone zone)
    {
        this.zone = zone;
        foreach (NPC npc in npcList)
        {
            npc.gameObject.SetActive(true);
        }
    }

    public override void UpdateEvent()
    {
        //Checks if all enemies are dead, in that case complete the event.
        bool check = true;
        foreach (NPC npc in npcList)
        {
            if (npc != null) { check = false; break; }
        }

        if (check) { CompletedEvent(); }
    }

    public override void CompletedEvent()
    {
        zone.NextEvent();
    }

    public override string ToString()
    {
        if (eventName == "") { return "(NPCEvent)"; }
        return $"{eventName} (NPCEvent)";
    }
}
