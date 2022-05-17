using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EventZone : MonoBehaviour
{
    [HideInInspector] public SphereCollider zoneCollider;
    [HideInInspector] public GameObject invertedCollider;
    [HideInInspector] public List<GameEvent> events = new List<GameEvent>();
    GameEvent currentEvent = null;

    void Start()
    {
        foreach (GameEvent _event in events) {
            _event.Init();
            foreach(Transform child in transform)
            {
                if(child.gameObject.tag == "InvertedCollider")
                {
                    invertedCollider = child.gameObject;
                }
            }
            if(invertedCollider.tag != "InvertedCollider") { invertedCollider = null; return; }
            var scaleChange = new Vector3(zoneCollider.radius, zoneCollider.radius, zoneCollider.radius);
            invertedCollider.transform.localScale = scaleChange;
        }
    }

    public void NextEvent()
    {
        if(events.Count == 0) { invertedCollider.SetActive(false); return; }
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

    public void SwapElements(int index1, int index2)
    {
        GameEvent holder = events[index1];
        events[index1] = events[index2];
        events[index2] = holder;
    }

    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") && currentEvent == null) {
            NextEvent();
        }
    }
}
