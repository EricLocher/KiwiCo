using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Quest.Dialogue;

public class NPCEvent : GameEvent
{
    [SerializeField] public List<DialogueTrigger> npcList;
    [SerializeField] string npcTrigger = "npc";

    public override void Init()
    {
        foreach (DialogueTrigger npc in npcList)
        {
            npc.gameObject.SetActive(false);
        }
    }

    public override void StartEvent(EventZone zone)
    {
        this.zone = zone;
        foreach (DialogueTrigger npc in npcList)
        {
            npc.gameObject.SetActive(true);
            if(npc.action == npcTrigger)
                npc.onTrigger.AddListener(CompletedEvent);
        }
    }

    public override void UpdateEvent()
    {
        bool check = true;
        foreach (DialogueTrigger npc in npcList)
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
