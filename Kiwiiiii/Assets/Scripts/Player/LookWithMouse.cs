using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    public void UpdateCamera(InputAction.CallbackContext ctx)
    {
        Vector2 mouseDelta = ctx.ReadValue<Vector2>();
        mouseDelta /= 5;
        //mouseDelta *= -1;
        transform.localEulerAngles += (Vector3.right * mouseDelta.y) + (Vector3.up * mouseDelta.x);
    }
}