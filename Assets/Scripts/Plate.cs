using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : KitchenWieldable
{
    public event EventHandler<OnDishCompleteEventArgs> OnDishComplete;
    public class OnDishCompleteEventArgs : EventArgs { public List<GameObject> CompleteDishVisualsList; }
    public event EventHandler<OnHeldItemsChangeEventArgs> OnHeldItemsChange;
    public class OnHeldItemsChangeEventArgs : EventArgs { public List<KitchenWieldableSO> itemsList; }
    public DishRecipeSO _finishedRecipe; //review later if needed
    [SerializeField] private List<KitchenWieldableSO> _acceptableItems;
    [SerializeField] private List<DishRecipeSO> _acceptableRecipes;
    private const int _MAX_ITEMS = 9;
    public List<KitchenWieldableSO> _heldItems;


    private void Start()
    {
        _heldItems = new List<KitchenWieldableSO>();
    }
    public bool TryPutIntoPlate(KitchenWieldable kitchenWieldable)
    {
        if (_acceptableItems.Contains(kitchenWieldable._kitchenWieldableSO) && _heldItems.Count < _MAX_ITEMS)
        {
            _heldItems.Add(kitchenWieldable._kitchenWieldableSO);
            OnHeldItemsChange?.Invoke(this, new OnHeldItemsChangeEventArgs { itemsList = _heldItems });
            kitchenWieldable.DestroySelf();
            CheckRecipeComplete();
            return true;
        }
        else return false;
    }

    private void CheckRecipeComplete()
    {
        foreach (DishRecipeSO recipe in _acceptableRecipes)
        {
            //if (ExtractListOfRequirements(_heldItems).SequenceEqual(ExtractListOfRequirements(recipe))) Debug.Log("Match!");
            if (ExtractListOfRequirements(_heldItems).SequenceEqual(ExtractListOfRequirements(recipe)))
            {
                _finishedRecipe = recipe;
                OnDishComplete?.Invoke(this, new OnDishCompleteEventArgs { CompleteDishVisualsList = recipe.CompletedRecipeVisualObjects });
            }
            else _finishedRecipe = null;
        }
    }

    //Creates a list of requirements in the DishRecipeSO in the order that items appear in the recipe (an Ingredient Requirement is a struct that contains an ingredient and an amount)
    public List<IngredientRequirement> ExtractListOfRequirements(List<KitchenWieldableSO> ingredientList)
    {
        List<IngredientRequirement> OutputList = new List<IngredientRequirement>();
        foreach (KitchenWieldableSO item in _acceptableItems)
        {
            if (CountItems(item, ingredientList) != 0)
                OutputList.Add(new IngredientRequirement { Ingredient = item, Amount = CountItems(item, ingredientList) });
        }
        return OutputList;
    }

    //Organizes the List of requirements in the DishRecipeSO in the order that items appear in the recipe
    public List<IngredientRequirement> ExtractListOfRequirements(DishRecipeSO recipe)
    {
        List<IngredientRequirement> OutputList = new List<IngredientRequirement>();

        foreach (KitchenWieldableSO item in _acceptableItems)
        {
            foreach (IngredientRequirement requirement in recipe.NeededIngredients)
            {
                if (requirement.Ingredient == item) OutputList.Add(requirement);
            }
        }
        return OutputList;
    }

    //counts the amount of items of T in a List of T
    public int CountItems<T>(T item, List<T> itemsList)
    {
        int amount = 0;
        foreach (T wieldable in itemsList)
            if (wieldable.Equals(item)) amount++;
        return amount;
    }
}
