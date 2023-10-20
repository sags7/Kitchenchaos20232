using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu()]
public class DishRecipeSO : ScriptableObject
{
    public List<IngredientRequirement> NeededIngredients;
    public List<GameObject> CompletedRecipeVisualObjects;
}