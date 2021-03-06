using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponsBehavior : MonoBehaviour
{
    public Transform character;
    public GameObject WeaponHolder;
    private void Start()
    {
        if(WeaponHolder == null)
        { Debug.Log("You need to assign WeaponHolder in inspector."); }
       
    }

    public void Sheath(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WeaponHolder.SetActive(!WeaponHolder.activeSelf);
        }
    }
}
