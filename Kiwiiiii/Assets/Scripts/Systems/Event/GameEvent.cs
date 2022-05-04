using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GameEvent : ScriptableObject
{
    [SerializeField]
    public string eventName;

    [HideInInspector]
    public bool Minimized = true;

    protected EventZone zone;

    public virtual void Init() { }
    public abstract void StartEvent(EventZone zone);
    public virtual void UpdateEvent() { }
    public abstract void CompletedEvent();
}
