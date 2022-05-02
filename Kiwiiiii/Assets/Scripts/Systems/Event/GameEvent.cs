using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GameEvent : ScriptableObject
{
    [SerializeField]
    public string EventName;
    public string EventID;

    protected EventZone zone;

    public abstract void StartEvent(EventZone zone);
    public abstract void UpdateEvent();
    public abstract void CompletedEvent();

}
