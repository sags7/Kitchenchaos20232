using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] public Transform _container;
    [SerializeField] public Transform _recipeTemplate;

    // private void Awake()
    // {

    //     DeliveryManager.Instance.OnNewOrderCreated += OnOrderCreatedAction;
    //     DeliveryManager.Instance.OnOrderDelivered += OnOrderDeliveredAction;
    //     Debug.Log("Beejeezus");
    // }
    private async void Start()
    {
        await Task.Delay(100);
        DeliveryManager.Instance.OnNewOrderCreated += OnOrderCreatedAction;
        DeliveryManager.Instance.OnOrderDelivered += OnOrderDeliveredAction;

        UpdateVisual();
    }

    private void OnOrderDeliveredAction(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void OnOrderCreatedAction(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (DishRecipeSO order in DeliveryManager.Instance._queuedOrders)
        {
            Transform newRecipe = Instantiate(_recipeTemplate, _container);
            newRecipe.gameObject.SetActive(true);
        }
    }
}
