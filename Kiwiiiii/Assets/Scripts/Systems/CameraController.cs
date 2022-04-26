using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

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
    [SerializeField, Range(0f, 180f)]
    float maxClampY = 60;
    [SerializeField, Range(-180f, 0f)]
    float minClampY = -60f;
    [SerializeField, Range(-90f, 0f)]
    float zoomDistance = -10f;
    [SerializeField]
    LayerMask layerMask;

    RaycastHit _camHit;
    Vector3 camDist;

    void Start()
    {
        camDist = cam.transform.localPosition;
        camDist.z = zoomDistance;
        Cursor.visible = false;
    }

    void Update()
    {
        cameraCenter.transform.position = new Vector3(target.transform.position.x,
            target.transform.position.y + yOffset, target.transform.position.z);

        CameraCollision();
    }

    public void MouseInput(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        var rotation = Quaternion.Euler(
            cameraCenter.transform.rotation.eulerAngles.x - input.y * sensitivity / 2,
            cameraCenter.transform.rotation.eulerAngles.y + input.x * sensitivity,
            cameraCenter.transform.rotation.eulerAngles.z);

        // fixa
        //rotation.x = Mathf.Clamp(rotation.x, minClampY, maxClampY);

        cameraCenter.transform.rotation = rotation;
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

        // justera
        if (cam.transform.localPosition.z > -1f)
        {
            cam.transform.localPosition =
                new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
        }
    }
}