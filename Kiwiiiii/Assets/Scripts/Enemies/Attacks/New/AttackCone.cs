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

        Vector3 coneForward = Quaternion.Euler(0, rotation, 0) * origin.forward;

        var firstAngle = Vector3.Angle(coneForward, dir);

        var newAngle = (angle / 2);

        Debug.Log("start: " + firstAngle);
        Debug.Log("second: " + newAngle);

        if (firstAngle < newAngle) {
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
