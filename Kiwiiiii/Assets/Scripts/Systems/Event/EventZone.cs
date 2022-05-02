using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EventZone : MonoBehaviour
{
    [HideInInspector] public SphereCollider zoneCollider;
    [HideInInspector] public List<GameEvent> events = new List<GameEvent>();

    public GameEvent currentEvent = null;


    public void NextEvent()
    {
        if(events.Count == 0) { return; }
        currentEvent = events[0];
        currentEvent.StartEvent(this);
        events.RemoveAt(0);
    }

    void Update()
    {
        currentEvent?.UpdateEvent();
    }

    #region Inspector Functions

    public void AddEvent(GameEvent _event)
    {
        events.Add(_event);
    }

    public void RemoveEvent(int index)
    {
        events.RemoveAt(index);
    }

    public void CheckDependency()
    {
        if (zoneCollider != null) { return; }
        zoneCollider = GetComponent<SphereCollider>();
    }

    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentEvent == null) {
            NextEvent();
        }
    }
}
