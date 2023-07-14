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
    private PlayerInteract interact;
    private PlayerMicrowave microwave;
    private GamePause pause;
    private PlayerPerspective perspective;

    private void Awake()
    {
        playerInput = new PlayerInput();
        controls = playerInput.Movement;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        interact = GetComponent<PlayerInteract>();
        microwave = GetComponent<PlayerMicrowave>();
        pause = GetComponent<GamePause>();
        perspective = GetComponent<PlayerPerspective>();

        controls.Jump.performed += ctx => movement.Jump();
        controls.Sprint.started += ctx => movement.ToggleSprint(true);
        controls.Sprint.canceled += ctx => movement.ToggleSprint(false);
        controls.Interact.started += ctx => interact.StartInteract();
        controls.Interact.canceled += ctx => interact.CancelInteract();
        controls.Dispose.performed += ctx => microwave.DisposeFood();
        controls.PauseGame.performed += ctx => pause.TogglePauseGame();
        controls.SwitchPerspective.performed += ctx => perspective.TogglePerspective();
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
