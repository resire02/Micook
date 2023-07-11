using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.MovementActions controls;

    private PlayerMovement movement;
    private PlayerLook look;

    private void Awake()
    {
        playerInput = new PlayerInput();
        controls = playerInput.Movement;
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        controls.Jump.performed += ctx => movement.Jump();
        controls.Sprint.started += ctx => movement.ToggleSprint(true);
        controls.Sprint.canceled += ctx => movement.ToggleSprint(false);
    }

    private void FixedUpdate()
    {
        movement.HandleMove(controls.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.HandleLook(controls.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public bool CheckInteractTriggered()
    {
        return controls.Interact.triggered;
    }

    public bool CheckSprintTriggered()
    {
        return controls.Sprint.triggered;
    }
}