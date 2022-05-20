using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayerController : Character
{
    [SerializeField] VisualEffect heal;
    public SOPlayerStats stats { get { return (SOPlayerStats)characterStats; } }

    //private void Start()
    //{
    //    //TODO: Make it so stats carry over to new scenes

    //    //playerStats = new SOPlayerStats();
    //    //ScriptableObject.CreateInstance<SOPlayerStats>();
    //    //(SOPlayerStats)CreateInstance(typeof(SOPlayerStats));

    //}

    protected override void Init()
    {
        heal.Stop();
    }

    private void Update()
    {
        //Debug.Log(playerStats.berriesInInventory);
    }

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);
        AudioManager.instance.PlayOnce("PlayerDamage");
    }

    protected override void OnDeath()
    {
        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        //Player Death thing.
    }

    public override void Heal(float value)
    {
        base.Heal(value);
        heal.Play();
        AudioManager.instance.PlayOnce("PlayerHeal");
    }
}
