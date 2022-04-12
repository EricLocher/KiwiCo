using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTemporary : MonoBehaviour
{
    public Vector3 PlayerMoveInput;

    private PlayerInput Controls;

    Rigidbody rb;

    public float speed;

    private void Start()
    {
        Controls = new PlayerInput();
        Controls.Enable();

        Controls.CharacterControls.Move.performed += ctx =>
        {
            PlayerMoveInput = new Vector3(ctx.ReadValue<Vector2>().x, PlayerMoveInput.y, ctx.ReadValue<Vector2>().y);
        };

        Controls.CharacterControls.Move.canceled += ctx =>
        {
            PlayerMoveInput = new Vector3(ctx.ReadValue<Vector2>().x, PlayerMoveInput.y, ctx.ReadValue<Vector2>().y);
        };

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        rb.AddForce(PlayerMoveInput * Time.deltaTime * speed);
    }



}
