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

    [Header("View Distance")]
    [SerializeField, Range(0, 30)]
    float MaxViewDistance = 15f;
    [SerializeField, Range(0, 30)]
    float MinViewDistance = 1f;

    [Header("Rates")]
    [SerializeField, Range(0, 30)]
    int ZoomRate = 20;
    [SerializeField, Range(0, 30)]
    int lerpRate = 5;

    float distance = 3f;
    float desireDistance;
    float correctedDistance;
    float currentDistance;
    float cameraX;

    [Header("Camera Height")]
    [SerializeField, Range(0, 30)]
    float cameraTargetHeight = 1.0f;

    [Header("Sensitivity")]
    [SerializeField, Range(0, 0.2f)]
    float sensitivity = 0.05f;

    void Start()
    {
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
        currentDistance = distance;
        desireDistance = distance;
        correctedDistance = distance;
    }

    void FixedUpdate()
    {
        y = ClampAngle(y, -15, 25);
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        desireDistance = Mathf.Clamp(desireDistance, MinViewDistance, MaxViewDistance);
        correctedDistance = desireDistance;

        Vector3 position = CameraTarget.position - (rotation * Vector3.forward * desireDistance);

        RaycastHit collisionHit;
        Vector3 cameraTargetPosition = new Vector3(CameraTarget.position.x, CameraTarget.position.y + cameraTargetHeight, CameraTarget.position.z);

        bool isCorrected = false;

        if (Physics.Linecast(cameraTargetPosition, position, out collisionHit))
        {
            position = collisionHit.point;
            correctedDistance = Vector3.Distance(cameraTargetPosition, position);
            isCorrected = true;
        }

        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * ZoomRate) : correctedDistance;

        position = CameraTarget.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -cameraTargetHeight, 0));

        transform.rotation = rotation;
        transform.position = position;

        cameraX = transform.rotation.x;
    }

    public void MouseInput(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        input *= sensitivity;

        x += input.x * mouseXSpeedMod;
        y += (input.y * -1) * mouseYSpeedMod;

        //CameraTarget.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);

        distance = distance - distance - 1;
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