using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [Header("Target to follow")]
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject cameraCenter;
    [SerializeField]
    Camera cam;

    [Header("Settings")]
    [SerializeField, Range(0f, 10f)]
    float yOffset = 1f;
    [SerializeField, Range(0.1f, 20f)]
    float sensitivity = 3f;
    [SerializeField, Range(0.1f, 20f)]
    float collisionSensitivity = 4.5f;
    [SerializeField, Range(-360f, 360f)]
    float maxClampY = 55;
    [SerializeField, Range(-360f, 360f)]
    float minClampY = -13;
    [SerializeField, Range(-90f, 0f)]
    float zoomDistance = -10f;
    [SerializeField]
    LayerMask layerMask;

    RaycastHit _camHit;
    Vector3 camDist;
    float y = 0f, x = 0f;
    Quaternion rotation;

    void Start()
    {
        if (Application.isPlaying) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        camDist = cam.transform.localPosition;
        camDist.z = zoomDistance;
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
    }

    void Update()
    {
        cameraCenter.transform.position = new Vector3(target.transform.position.x,
            target.transform.position.y + yOffset, target.transform.position.z);

        y = ClampAngle(y, -13, 55);
        rotation = Quaternion.Euler(y, x, 0);
        cameraCenter.transform.rotation = rotation;

        if(Application.isPlaying)
            CameraCollision();
    }

    public void MouseInput(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        input.x *= sensitivity;
        input.y *= sensitivity / 2;

        x += input.x;
        y += (input.y * -1);
    }

    void CameraCollision()
    {
        var transform2 = cam.transform;
        transform2.localPosition = camDist;

        GameObject obj = new GameObject();
        obj.transform.SetParent(transform2.parent);
        var position = cam.transform.localPosition;
        obj.transform.localPosition = new Vector3(position.x, position.y, position.z - collisionSensitivity);

        if (Physics.Linecast(cameraCenter.transform.position, obj.transform.position, out _camHit))
        {

            var transform1 = cam.transform;
            transform1.position = _camHit.point;
            var localPosition = transform1.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z + collisionSensitivity);
            transform1.localPosition = localPosition;
        }

        Destroy(obj);
    }

    static float ClampAngle(float angle, float min, float max)
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