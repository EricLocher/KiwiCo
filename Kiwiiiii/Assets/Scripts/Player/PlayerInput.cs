using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerMovement movement;
    private WeaponsBehavior weapons;
    private LookWithMouse lookWithMouse;
    private CameraController cameraController;
    private InteractController interactController;
    [SerializeField] private HotbarBehavior hotbar;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        weapons = GetComponent<WeaponsBehavior>();
        controls = new PlayerControls();
        lookWithMouse = GetComponent<LookWithMouse>();
        cameraController = Camera.main.GetComponent<CameraController>();
        interactController = GetComponent<InteractController>();

   
        controls.Player.Mouse.performed += ctx => cameraController?.MouseInput(ctx);
        controls.Player.Jump.performed += ctx => movement.Jump(ctx);
        controls.Player.Dash.performed += ctx => movement.Dash();
        controls.Player.Spin.performed += ctx => movement.Spin();
        controls.Player.Mouse.performed += ctx => lookWithMouse.UpdateCamera(ctx);
        controls.Player.Sheath.performed += ctx => weapons.Sheath(ctx);
        controls.Player.Interact.performed += ctx => interactController.Interact(ctx);
        controls.Player.Hotbar2.performed += ctx => hotbar.UseItem(1);
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
            movement.Spin();
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

        this.movement.Move(movement);
    }

    public void SheathWeapon()
    {

    }

    public void Equip()
    {
      
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
}