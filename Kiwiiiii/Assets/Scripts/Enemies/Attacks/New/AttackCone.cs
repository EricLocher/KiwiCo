using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCone : ScriptableObject
{
    [HideInInspector] public Transform origin;

    [Header("Attack Cone Settings")]
    [Range(0, 50)] public float radius = 1;
    [Range(0, 360)] public float angle = 1;
    [Range(0, 360)] public float rotation = 1;


    public bool TargetInCone(Transform target)
    {
        float dist = Vector3.Distance(origin.position, target.position);
        if (dist > radius) { return false; }

        Vector3 dir = (target.position - origin.position).normalized;

        
        if (Vector3.Angle(origin.forward, dir) < angle / 2) {
            return true;
        }
        return false;
    }

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        angleInDegrees += origin.eulerAngles.y;

        return new Vector3(Mathf.Sin((rotation + angleInDegrees) * Mathf.Deg2Rad), 0, Mathf.Cos((rotation + angleInDegrees) * Mathf.Deg2Rad));
    }


}
