using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerMovement controller;
    private LookWithMouse lookWithMouse;
    private CameraController cameraController;

    private void Awake()
    {
        controller = GetComponent<PlayerMovement>();
        controls = new PlayerControls();
        lookWithMouse = GetComponent<LookWithMouse>();
        cameraController = Camera.main.GetComponent<CameraController>();
        controls.Player.Mouse.performed += ctx => cameraController?.MouseInput(ctx);

        controls.Player.Jump.performed += ctx => controller.Jump(ctx);
        controls.Player.Dash.performed += ctx => controller.Dash();
        controls.Player.Spin.performed += ctx => controller.Spin();
        controls.Player.Mouse.performed += ctx => lookWithMouse.UpdateCamera(ctx);
    }

    private void Update()
    {
        //if (controls.Player.Jump.triggered)
        //{
        //    controller.Jump();
        //}
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Spin(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Spin();
            //transform.Rotate(0f, 180f, 0f, Space.Self);
        }

    }

    public void Move()
    {
        var movementInput = controls.Player.Movement.ReadValue<Vector2>();

        var movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;

        controller.Move(movement);
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
}