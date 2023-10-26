using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DishRecipeSO : ScriptableObject
{
    public string _orderName;
    public List<IngredientRequirement> NeededIngredients;
    public List<GameObject> CompletedRecipeVisualObjects;
}
