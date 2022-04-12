using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    public float speed;
    private Rigidbody rb;
    Vector2 velocity = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(velocity.x, 0.0f, velocity.y);

        characterController.Move(movement * speed);
    }

    public void Move(InputAction.CallbackContext value)
    {
        velocity = value.ReadValue<Vector2>();
    }

}
