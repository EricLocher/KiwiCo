using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform CameraTarget;

    float x = 0.0f;
    float y = 0.0f;

    int mouseXSpeedMod = 5;
    int mouseYSpeedMod = 5;

    [Header("FOV")]
    [SerializeField, Range(0, 30)]
    float currentDistance = 5f;

    float maxDistance;
    float desiredDistance;

    [Header("Lerp rate")]
    [SerializeField, Range(0, 50)]
    float lerpRate = 0.3f;

    [Header("Camera Height")]
    [SerializeField, Range(0, 30)]
    float cameraTargetHeight = 1.0f;

    [Header("Sensitivity")]
    [SerializeField, Range(0, 0.2f)]
    float sensitivity = 0.05f;

    [Header("Layermask")]
    [SerializeField]
    LayerMask layerMask;

    float radius = 0.5f;
    Vector3 currentDeltaVelocity = Vector3.zero;
    Vector3 currentRotationDeltaVelocity = Vector3.zero;
    Vector3 position;
    Vector3 rotation;

    void Start()
    {
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
        maxDistance = currentDistance;
    }

    void FixedUpdate()
    {
        CameraCollision();

        y = ClampAngle(y, -15, 25);

        rotation = new Vector3(y, x, 0);

        position = CameraTarget.position - (Quaternion.Euler(rotation) * Vector3.forward * desiredDistance + new Vector3(0, -cameraTargetHeight, 0));

        transform.eulerAngles = rotation;
        //transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, rotation, ref currentRotationDeltaVelocity, lerpRate, Mathf.Infinity, Time.fixedDeltaTime);
        //transform.position = Vector3.Lerp(transform.position, position, lerpRate * Time.fixedDeltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, position, ref currentDeltaVelocity, lerpRate, Mathf.Infinity, Time.fixedDeltaTime);
    }

    void CameraCollision()
    {
        //Check if view obstructed
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hitColliders.Length != 0)
        {
            foreach (var hit in hitColliders)
            {
                Vector3 closestPoint = hit.ClosestPointOnBounds(transform.position);
                desiredDistance = Vector3.Distance(transform.position, closestPoint) + 1;
                //currentDistance -= desiredDistance + 1f;
            }
        }
        else
        {
            desiredDistance = maxDistance;
        }
    }

    public void MouseInput(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        input *= sensitivity;

        x += input.x * mouseXSpeedMod;
        y += (input.y * -1) * mouseYSpeedMod;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}