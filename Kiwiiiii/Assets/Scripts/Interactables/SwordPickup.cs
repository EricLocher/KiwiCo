using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SwordPickup : MonoBehaviour
{
    [SerializeField] VisualEffect system;
    [SerializeField] Transform slashPoint;

    private void FixedUpdate()
    {
        system.SetVector3("pos", slashPoint.position);
    }

}
