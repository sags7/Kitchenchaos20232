using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnNewOrderCreated;
    public event EventHandler OnOrderDelivered;

    [SerializeField] private DeliveryCounter _deliveryCounter;
    public List<DishRecipeSO> _queuedOrders { get; private set; }
    [SerializeField] private List<DishRecipeSO> _possibleOrders;
    [SerializeField] private float _orderInterval = 5f;
    private float _newOrderTimer = 0;
    private int _maxQueue = 5;

    public static DeliveryManager Instance { get; private set; }

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        _queuedOrders = new List<DishRecipeSO>();
        _deliveryCounter.OnPlateDelivered += OnPlateDeliveredAction;
    }

    // private void OnPlateDeliveredAction(object sender, DeliveryCounter.OnPlateDeliveredEventArgs args)
    // {
    //     Plate plate = args._plateDelivered;
    //     bool recipeFound = false;
    //     foreach (DishRecipeSO recipe in _queuedOrders)
    //     {
    //         if (recipeFound == false && plate.ExtractListOfRequirements(plate._heldItems).SequenceEqual(plate.ExtractListOfRequirements(recipe)))
    //         {
    //             Debug.Log("<color=green> Correct Recipe!</color>");
    //             _queuedOrders.Remove(recipe);
    //             args._plateDelivered.DestroySelf();
    //             recipeFound = true;
    //             _newOrderTimer = 0;
    //             break;
    //         }
    //     }
    //     if (recipeFound == false) Debug.Log("<color=red>Incorrect Recipe!!!</color>");
    // }
    private void OnPlateDeliveredAction(object sender, DeliveryCounter.OnPlateDeliveredEventArgs args)
    {
        bool recipeFound = false;
        for (int i = 0; i < _queuedOrders.Count; i++)
        {
            if (recipeFound == false && args._plateDelivered.ExtractListOfRequirements(args._plateDelivered._heldItems).SequenceEqual(args._plateDelivered.ExtractListOfRequirements(_queuedOrders[i])))
            {
                //Debug.Log("<color=green> Correct Recipe!</color>");
                OnOrderDelivered?.Invoke(this, EventArgs.Empty);
                _queuedOrders.RemoveAt(i);
                args._plateDelivered.DestroySelf();
                recipeFound = true;
                _newOrderTimer = 0;
                break;
            }
        }
        if (recipeFound == false)
        {
            Debug.Log("<color=red>Incorrect Recipe!!!</color>");
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
            //foreach (DishRecipeSO order in _queuedOrders) Debug.Log(order._orderName);
        }
        if (_newOrderTimer >= _orderInterval) _newOrderTimer = 0;
    }
}
