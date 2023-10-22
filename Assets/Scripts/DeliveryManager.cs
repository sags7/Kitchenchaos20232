using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private DeliveryCounter _deliveryCounter;
    [SerializeField] public List<DishRecipeSO> _queuedOrders;
    [SerializeField] private List<DishRecipeSO> _possibleOrders;
    private float _newOrderTimer;
    private float _orderInterval = 1.5f;
    private int _maxQueue = 5;

    private void Start()
    {
        _queuedOrders = new List<DishRecipeSO>();
        _deliveryCounter.OnPlateDelivered += OnPlateDeliveredAction;
    }

    private void OnPlateDeliveredAction(object sender, DeliveryCounter.OnPlateDeliveredEventArgs args)
    {
        bool recipeFound = false;
        foreach (DishRecipeSO order in _queuedOrders)
        {
            if (recipeFound == false && args._plateDelivered.ExtractListOfRequirements(args._plateDelivered._heldItems).SequenceEqual(args._plateDelivered.ExtractListOfRequirements(order)))
            {
                Debug.Log("<color=green> Correct Recipe!</color>");
                args._plateDelivered.DestroySelf();
                recipeFound = true;
            }
        }
        if (recipeFound == false) Debug.Log("<color=red>Incorrect Recipe!!!</color>");
    }

    private void Update()
    {
        _newOrderTimer += Time.deltaTime;

        if (_newOrderTimer >= _orderInterval && _queuedOrders.Count <= _maxQueue)
        {
            _newOrderTimer = 0;
            _queuedOrders.Add(_possibleOrders[Random.Range(0, _possibleOrders.Count)]);
            foreach (DishRecipeSO order in _queuedOrders) Debug.Log(order._orderName);
        }
    }
}
