using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Character
{
    public SOPlayerStats stats { get { return (SOPlayerStats)characterStats; } }

    //private void Start()
    //{
    //    //TODO: Make it so stats carry over to new scenes
       
    //    //playerStats = new SOPlayerStats();
    //    //ScriptableObject.CreateInstance<SOPlayerStats>();
    //    //(SOPlayerStats)CreateInstance(typeof(SOPlayerStats));

    //}

    private void Update()
    {
        //Debug.Log(playerStats.berriesInInventory);
    }

    protected override void OnDeath()
    {
        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        //Player Death thing.
    }
}
