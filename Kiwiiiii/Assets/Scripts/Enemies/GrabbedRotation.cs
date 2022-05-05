using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedRotation : MonoBehaviour
{

    public Transform RootPos;
    public Transform RotateTowards;
    public Vector3 extraRot;
   

    void Update()
    {
        this.transform.position = RootPos.position;
        this.transform.LookAt(RotateTowards);
        this.transform.Rotate(extraRot);
    

    }
}
