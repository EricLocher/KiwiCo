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

        #region Input bindings

        //Hold to stop camera rotation
        controls.Player.Mouse1.started += ctx => cameraController?.OnMouseDown(true);
        controls.Player.Mouse1.canceled += ctx => cameraController?.OnMouseDown(false);

        //Camera Rotation mouse delta input
        controls.Player.Mouse.performed += ctx => cameraController?.MouseInput(ctx);

        //Jump
        controls.Player.Jump.performed += ctx => movement.Jump(ctx);

        //Dash
        controls.Player.Dash.performed += ctx => movement.Dash(ctx);

        //Point Sword Down
        controls.Player.PointDown.started += ctx => lookWithMouse.PointDown();

        //Sheath sword
        controls.Player.Sheath.performed += ctx => weapons.Sheath(ctx);

        //Interact with interactable object
        controls.Player.Interact.performed += ctx => interactController.Interact(ctx);

        //Hotbar
        controls.Player.Hotbar2.performed += ctx => hotbar.UseItem(1);
        controls.Player.Schlam.performed += ctx => movement.Schlam(ctx);

        #endregion
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