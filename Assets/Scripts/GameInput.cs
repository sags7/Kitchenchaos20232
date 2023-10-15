using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();  
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += performThis;
    }

    private void performThis(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() => playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
}
