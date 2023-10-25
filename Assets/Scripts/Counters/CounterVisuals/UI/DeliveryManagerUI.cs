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

    private void Start()
    {
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
        ClearVisual();
        foreach (DishRecipeSO order in DeliveryManager.Instance._queuedOrders)
        {
            Transform newRecipe = Instantiate(_recipeTemplate, _container);
            newRecipe.gameObject.SetActive(true);
            newRecipe.GetComponent<RecipeCardUI>().UpdateName(order);
        }
    }

    private void ClearVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTemplate) continue;
            //Debug.Log("Destroyed" + child);
            Destroy(child.gameObject);
        }
    }
}
