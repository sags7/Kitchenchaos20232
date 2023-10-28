using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PREFS_KEY_BINDINGS = "InputBindings";
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteract;
    public event EventHandler OnAlternateInteract;
    public event EventHandler OnGamePaused;
    private PlayerInputActions playerInputActions;
    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternative,
        Pause
    }


    public void RebindKey(Binding binding, Action actionAfterRebound)
    {
        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternative:
                inputAction = playerInputActions.Player.AlternateInteract;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        playerInputActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            playerInputActions.Player.Enable();
            callback.Dispose();
            actionAfterRebound();
            PlayerPrefs.SetString(PREFS_KEY_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }


    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        if (PlayerPrefs.HasKey(PREFS_KEY_BINDINGS)) playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PREFS_KEY_BINDINGS));
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += OnInteractAction;
        playerInputActions.Player.AlternateInteract.performed += OnAlternateInteractAction;
        playerInputActions.Player.Pause.performed += OnPauseAction;

        if (PlayerPrefs.HasKey(PREFS_KEY_BINDINGS)) playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PREFS_KEY_BINDINGS));
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= OnInteractAction;
        playerInputActions.Player.AlternateInteract.performed -= OnAlternateInteractAction;
        playerInputActions.Player.Pause.performed -= OnPauseAction;

        playerInputActions.Dispose();
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

    public string GetBindingName(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternative:
                return playerInputActions.Player.AlternateInteract.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }
}
