using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteract;
    public event EventHandler OnAlternateInteract;
    public event EventHandler OnGamePaused;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += OnInteractAction;
        playerInputActions.Player.AlternateInteract.performed += OnAlternateInteractAction;
        playerInputActions.Player.Pause.performed += OnPauseAction;
    }

    private void OnPauseAction(InputAction.CallbackContext context)
    {
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    private void OnAlternateInteractAction(InputAction.CallbackContext context)
    {
        OnAlternateInteract?.Invoke(this, EventArgs.Empty);
    }

    private void OnInteractAction(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() => playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
}
