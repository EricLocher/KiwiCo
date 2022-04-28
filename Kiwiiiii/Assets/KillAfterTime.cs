using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    public float timeUntilKill;
    void Start()
    {
        if (timeUntilKill == 0)
        {
            timeUntilKill = 5;
        }
    }
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
