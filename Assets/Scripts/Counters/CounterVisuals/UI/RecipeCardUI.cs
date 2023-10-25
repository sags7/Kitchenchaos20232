using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeCardUI : MonoBehaviour
{
    [SerializeField] private DeliveryManagerUI _deliveryManagerUI;
    [SerializeField] private Transform _cardName;

    public void UpdateName(DishRecipeSO dishRecipeSO)
    {
        _cardName.GetComponent<TextMeshProUGUI>().text = dishRecipeSO._orderName;
    }
}
