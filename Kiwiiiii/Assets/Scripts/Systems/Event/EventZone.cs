using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EventZone : MonoBehaviour
{
    public GameObject invertedColliderPrefab;
    public bool barrier = false;
    [HideInInspector] public SphereCollider zoneCollider;
    [HideInInspector] public List<GameEvent> events = new List<GameEvent>();
    [HideInInspector] public GameObject invertedCollider;
    GameEvent currentEvent = null;

    bool hasStarted = false;

    void Start()
    {
        foreach (GameEvent _event in events) {
            _event.Init();
        }
    }

    public void NextEvent()
    {
        if (events.Count == 0) {
            AudioManager.instance.PlayOnce("PlayerCompleteEvent");
            Destroy(gameObject);
        }
        else {
            AudioManager.instance.PlayOnce("PlayerEnterEvent");
            currentEvent = events[0];
            currentEvent.StartEvent(this);
            events.RemoveAt(0);

            if (!hasStarted) {
                if (barrier) {
                    invertedCollider.SetActive(true);
                    var scaleChange = new Vector3(zoneCollider.radius, zoneCollider.radius, zoneCollider.radius);
                    invertedCollider.transform.localScale = scaleChange * 2.5f;
                }
                hasStarted = true;
            }
        }
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
        if (zoneCollider == null)
            zoneCollider = GetComponent<SphereCollider>();
        if (barrier && invertedCollider == null) {
            invertedCollider = Instantiate(invertedColliderPrefab, transform);
            invertedCollider.SetActive(false);
        }
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
