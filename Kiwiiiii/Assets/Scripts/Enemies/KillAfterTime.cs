using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    [SerializeField, Range(1,10)] private float timeUntilKill;

    void Update()
    {
        timeUntilKill -= Time.deltaTime;

        if (timeUntilKill <= 0)
        {
            KillMe();
        }
    }

    private void KillMe()
    {
        Destroy(this.gameObject);
    }
}
