using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Minimap : MonoBehaviour
{
    GameObject target;
    [SerializeField] float yOffset = 20f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Character");
    }

    void Update()
    {
        GetComponent<Camera>().orthographicSize = yOffset;

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, target.transform.position.z);
    }
}
