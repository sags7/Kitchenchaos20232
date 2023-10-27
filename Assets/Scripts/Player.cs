using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenWieldableParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs { public BaseCounter selectedCounter; }
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _playerRadius = 0.6f;
    [SerializeField] private float _playerHeight = 1f;
    [SerializeField] private LayerMask _countersLayerMask;

    private bool _isWalking = false;
    private Vector3 _interactDirection;
    private Vector2 _inputVector;
    private BaseCounter _selectedCounter;

    private void Start()
    {
        Instance = this;

        _gameInput.OnInteract += OnInteractAction;
        _gameInput.OnAlternateInteract += OnAlternateInteractAction;
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
        if (_selectedCounter && GameManager.Instance.IsGamePlaying) _selectedCounter.Interacted(this);
    }

    private void OnAlternateInteractAction(object sender, EventArgs e)
    {
        if (_selectedCounter && GameManager.Instance.IsGamePlaying) _selectedCounter.AlternativeInteracted(this);
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
                moveDir = new Vector3(moveDir.x, 0, 0);
                Move(moveDir, moveDistance);
            }
            else if (moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, new Vector3(0, 0, moveDir.z), moveDistance))
            {
                moveDir = new Vector3(0, 0, moveDir.z);
                Move(moveDir, moveDistance);

            }
        }
        else Move(moveDir, moveDistance);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, _rotateSpeed * Time.deltaTime);
    }

    private void Move(Vector3 moveDir, float moveDistance)
    {
        if (moveDir != Vector3.zero) _isWalking = true;
        else _isWalking = false;
        transform.position += moveDir * moveDistance;
    }

    public bool IsWalking() => _isWalking;

    //--------------------------IKitchenWieldable implementation-----------------------------------------
    [field: SerializeField] public Transform SpawnPoint { get; set; }
    public KitchenWieldable KitchenWieldableHeld { get; set; }

    public void SwapWieldablesWith(IKitchenWieldableParent newParent)
    {
        KitchenWieldable newParentItem = newParent.KitchenWieldableHeld ? newParent.KitchenWieldableHeld : null;
        KitchenWieldable playerItem = KitchenWieldableHeld ? KitchenWieldableHeld : null;
        if (newParentItem) newParentItem.Set_ParentHoldingMe(this);
        KitchenWieldableHeld = newParentItem;
        if (playerItem) playerItem.Set_ParentHoldingMe(newParent);
        newParent.KitchenWieldableHeld = playerItem;
    }

    public bool TryPopulatePlate(IKitchenWieldableParent otherParent)
    {
        if (IKitchenWieldableParent.TryIsHolding<Plate>(otherParent.KitchenWieldableHeld, out Plate otherParentsPlate)
        && KitchenWieldableHeld ? !IKitchenWieldableParent.TryIsHolding<Plate>(KitchenWieldableHeld, out _) : false)
        {
            //Debug.Log("otherParent is holding a plate AND there is something other than a plate in me (this)");
            return otherParentsPlate.TryPutIntoPlate(KitchenWieldableHeld) ? true : false;
        }
        else return false;
    }
}
