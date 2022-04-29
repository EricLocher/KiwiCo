using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] WeaponsBehavior weapons;
    [SerializeField] ControlWeapon lookWithMouse;
    [SerializeField] InteractController interactController;
    [SerializeField] HotbarBehavior hotbar;

    PlayerControls controls;
    CameraController cameraController;

    private void Awake()
    {
        controls = new PlayerControls();
        cameraController = Camera.main.GetComponent<CameraController>();
   
        controls.Player.Mouse.performed += ctx => cameraController?.MouseInput(ctx);
        controls.Player.Jump.performed += ctx => movement.Jump(ctx);
        controls.Player.Dash.performed += ctx => movement.Dash(ctx);
        controls.Player.PointDown.started += ctx => lookWithMouse.PointDown();
        controls.Player.Sheath.performed += ctx => weapons.Sheath(ctx);
        controls.Player.Interact.performed += ctx => interactController.Interact(ctx);
        controls.Player.Hotbar2.performed += ctx => hotbar.UseItem(1);
        controls.Player.Schlam.performed += ctx => movement.Schlam(ctx);
    }

    private void FixedUpdate()
    {
        Move();
        lookWithMouse.MouseInput(controls.Player.Mouse.ReadValue<Vector2>());
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

    private void OnEnable() => controls?.Player.Enable();
    private void OnDisable() => controls?.Player.Disable();
}