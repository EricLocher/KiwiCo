using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    [SerializeField, Range(1,10)] private float timeUntilKill;

    private void Start()
    {
        Destroy(gameObject, timeUntilKill);
    }

}
