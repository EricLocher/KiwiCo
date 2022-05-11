using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHolder : Singleton<TempHolder>
{
    public static new Transform transform;

    protected override void Awake()
    {
        transform = gameObject.transform;
        destroyOnLoad = true;
        base.Awake();
    } 
}
