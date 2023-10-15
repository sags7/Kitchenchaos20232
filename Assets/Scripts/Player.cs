using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenWieldableParent
{
    public static Player Instance { get; private set; }
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _playerRadius = 0.7f;
    [SerializeField] private float _playerHeight = 1f;
    [SerializeField] private LayerMask _countersLayerMask;

    private bool _isWalking = false;
    private Vector3 _interactDirection;
    private Vector2 _inputVector;
    private BaseCounter _selectedCounter;

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs { public BaseCounter selectedCounter; }

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Error: There is more than one instance of Player class");
        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteract += OnInteractAction;
        _gameInput.OnAlternateInteract += OnAlternateInteractAction;
    }

    private void OnAlternateInteractAction(object sender, EventArgs e)
    {
        if (_selectedCounter) _selectedCounter.AlternativeInteracted(this);
    }

    private void Update()
    {
        _inputVector = _gameInput.GetMovementVectorNormalized();
        HandleMovement();
        UpdateInteractDirection();
        HandleInteractions();
    }

    private void UpdateInteractDirection()
    {
        if (_inputVector != Vector2.zero)
            _interactDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
    }

    private void HandleInteractions()
    {
        float _originOffsetY = 1f;
        float interactDistance = 1f;
        if (Physics.Raycast(transform.position + new Vector3(0, _originOffsetY, 0), _interactDirection, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
                SetSelectedCounter(baseCounter);
            else SetSelectedCounter(null);
        }
        else SetSelectedCounter(null);
    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        if (baseCounter != _selectedCounter)
        {
            _selectedCounter = baseCounter;
            OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs { selectedCounter = baseCounter });
        }
    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        if (_selectedCounter) _selectedCounter.Interacted(this);
    }

    private void HandleMovement()
    {
        Vector3 moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);
        float moveDistance = _movementSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            if (moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, new Vector3(moveDir.x, 0, 0), moveDistance))
            {
                moveDir = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = true;
            }
            else if (moveDir.y != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, new Vector3(0, 0, moveDir.z), moveDistance))
            {
                moveDir = new Vector3(0, 0, moveDir.z).normalized;
                canMove = true;
            }
        }
        else
        {
            if (moveDir != Vector3.zero) _isWalking = true;
            else _isWalking = false;
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, _rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking() => _isWalking;

    //--------------------------IKitchenWieldable implementation-----------------------------------------
    [field: SerializeField] public Transform SpawnPoint { get; set; }
    public KitchenWieldable KitchenWieldableHeld { get; set; }

    public void TransferWieldableTo(IKitchenWieldableParent newParent)
    {
        //copied from BaseCounter implementation(untested)
        KitchenWieldable newParentItem = newParent.KitchenWieldableHeld ? newParent.KitchenWieldableHeld : null;
        KitchenWieldable playerItem = KitchenWieldableHeld ? KitchenWieldableHeld : null;
        if (newParentItem) newParentItem.Set_ParentHoldingMe(this);
        KitchenWieldableHeld = newParentItem;
        if (playerItem) playerItem.Set_ParentHoldingMe(newParent);
        newParent.KitchenWieldableHeld = playerItem;
    }
}
