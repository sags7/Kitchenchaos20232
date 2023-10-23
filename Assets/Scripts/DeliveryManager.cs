using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private DeliveryCounter _deliveryCounter;
    private List<DishRecipeSO> _queuedOrders;
    [SerializeField] private List<DishRecipeSO> _possibleOrders;
    [SerializeField] private float _orderInterval = 5f;
    private float _newOrderTimer = 0;
    private int _maxQueue = 5;

    private void Start()
    {
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
                Debug.Log("<color=green> Correct Recipe!</color>");
                _queuedOrders.RemoveAt(i);
                args._plateDelivered.DestroySelf();
                recipeFound = true;
                _newOrderTimer = 0;
                break;
            }
        }
        if (recipeFound == false) Debug.Log("<color=red>Incorrect Recipe!!!</color>");
    }

    private void Update()
    {
        _newOrderTimer += Time.deltaTime;

        if (_newOrderTimer >= _orderInterval && _queuedOrders.Count < _maxQueue)
        {
            _newOrderTimer = 0;
            _queuedOrders.Add(_possibleOrders[Random.Range(0, _possibleOrders.Count)]);
            foreach (DishRecipeSO order in _queuedOrders) Debug.Log(order._orderName);
        }
        if (_newOrderTimer >= _orderInterval) _newOrderTimer = 0;
    }
}
