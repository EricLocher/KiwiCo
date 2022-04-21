using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SOPlayerStats playerStats;

    private void Start()
    {
        //TODO: Make it so stats carry over to new scenes
        playerStats = ScriptableObject.CreateInstance<SOPlayerStats>();
         //(SOPlayerStats)CreateInstance(typeof(SOPlayerStats));

    }
}
