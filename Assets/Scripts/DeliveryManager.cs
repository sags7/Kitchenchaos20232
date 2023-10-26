using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    private static DeliveryManager _instance;
    public static DeliveryManager Instance
    {
        get
        {
            if (!_instance) _instance = new DeliveryManager();
            return _instance;
        }
        private set { }
    }

    public event EventHandler OnNewOrderCreated;
    public event EventHandler OnOrderDelivered;
    public event EventHandler OnOrderSuccess;
    public event EventHandler OnOrderFail;
    [SerializeField] private DeliveryCounter _deliveryCounter;
    public List<DishRecipeSO> _queuedOrders { get; private set; }
    [SerializeField] private List<DishRecipeSO> _possibleOrders;
    [SerializeField] private float _orderInterval = 5f;
    private float _newOrderTimer = 0;
    private int _maxQueue = 5;


    private void Start()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);

        _queuedOrders = new List<DishRecipeSO>();
        _deliveryCounter.OnPlateDelivered += OnPlateDeliveredAction;
    }

    private void OnPlateDeliveredAction(object sender, DeliveryCounter.OnPlateDeliveredEventArgs args)
    {
        bool recipeFound = false;
        for (int i = 0; i < _queuedOrders.Count; i++)
        {
            if (recipeFound == false && args._plateDelivered.ExtractListOfRequirements(args._plateDelivered._heldItems).SequenceEqual(args._plateDelivered.ExtractListOfRequirements(_queuedOrders[i])))
            {
                //Delivery Success
                _queuedOrders.RemoveAt(i);
                OnOrderDelivered?.Invoke(this, EventArgs.Empty);
                OnOrderSuccess?.Invoke(this, EventArgs.Empty);
                args._plateDelivered.DestroySelf();
                recipeFound = true;
                _newOrderTimer = 0;
                break;
            }
        }
        if (recipeFound == false)
        {
            //Delivery Fail
            OnOrderFail?.Invoke(this, EventArgs.Empty);
            args._plateDelivered.DestroySelf();
        }
    }

    private void Update()
    {
        HandleOrderCreation();
    }

    private void HandleOrderCreation()
    {
        _newOrderTimer += Time.deltaTime;

        if (_newOrderTimer >= _orderInterval && _queuedOrders.Count < _maxQueue)
        {
            _newOrderTimer = 0;
            _queuedOrders.Add(_possibleOrders[UnityEngine.Random.Range(0, _possibleOrders.Count)]);
            OnNewOrderCreated?.Invoke(this, EventArgs.Empty);
        }
        if (_newOrderTimer >= _orderInterval) _newOrderTimer = 0;
    }
}
