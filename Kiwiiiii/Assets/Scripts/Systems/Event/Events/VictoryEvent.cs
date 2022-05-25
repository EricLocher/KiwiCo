using UnityEngine;

public class VictoryEvent : GameEvent
{
    [SerializeField] GameObject princessObj;
    Princess princess;

    public override void Init()
    {
        princess = princessObj.GetComponent<Princess>();
    }

    public override void StartEvent(EventZone zone)
    {
        princess.EndGame();
    }

    public override void UpdateEvent() {}

    public override void CompletedEvent()
    {
        AudioManager.instance.PlayOnce("PlayerCompleteEvent");
    }

    public override string ToString()
    {
        if (eventName == "") { return "(VictoryEvent)"; }
        return $"{eventName} (VictoryEvent)";
    }
}
