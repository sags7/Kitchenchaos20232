using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] public Transform _recipeCardContainer;
    [SerializeField] public Transform _recipeTemplate;

    private void Start()
    {
        DeliveryManager.Instance.OnNewOrderCreated += OnOrderCreatedAction;
        DeliveryManager.Instance.OnOrderDelivered += OnOrderDeliveredAction;
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
            Transform newRecipe = Instantiate(_recipeTemplate, _recipeCardContainer);
            newRecipe.gameObject.SetActive(true);
            newRecipe.GetComponent<RecipeCardUI>().UpdateName(order);
        }
    }

    private void ClearVisual()
    {
        foreach (Transform child in _recipeCardContainer)
        {
            if (child == _recipeTemplate) continue;
            Destroy(child.gameObject);
        }
    }
}
