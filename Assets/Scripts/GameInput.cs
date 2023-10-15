using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler OnAlternateInteract;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += OnInteractAction;
        playerInputActions.Player.AlternateInteract.performed += OnAlternateInteractAction;
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
