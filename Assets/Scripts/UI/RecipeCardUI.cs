using TMPro;
using UnityEngine;

public class RecipeCardUI : MonoBehaviour
{
    [SerializeField] private Transform _cardName;
    [SerializeField] private Transform _ingredientContainer;

    public void UpdateName(DishRecipeSO dishRecipeSO)
    {
        _cardName.GetComponent<TextMeshProUGUI>().text = dishRecipeSO._orderName;
        _ingredientContainer.GetComponent<IngredientContainerUI>().CreateIngredientIcons(dishRecipeSO.NeededIngredients);
    }
}
