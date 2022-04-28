using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Minimap : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float yOffset = 12f;

    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, target.transform.position.z);
    }
}
