using UnityEditor;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    // public event EventHandler OnSpawnedItem; //Unimplemented Event
    [SerializeField] private RecipeSO[] _availableRecipesArr;
    public override void AlternativeInteracted(Player player)
    {
        if (KitchenWieldableHeld)
        {
            try { AttemptRecipes(_availableRecipesArr); }
            catch { Debug.Log("Item has no recipes"); }
        }
        else Debug.Log("No Ingredients for recipe");
    }

    internal void AttemptRecipes(RecipeSO[] availableRecipesArr)
    {
        foreach (RecipeSO recipe in availableRecipesArr)
        {
            if (KitchenWieldableHeld._kitchenWieldableSO == recipe.inputs[0]) TransmuteTo(recipe.output);
            //Debug.Log("AttemptedRecipe:" + recipe.inputs[0] + "Ingredient: " + KitchenWieldableHeld._kitchenWieldableSO);

            //for each ingredient in the current recipe, check if its available
            //then create output if true and destroy ingredients
        };
    }

    private void TransmuteTo(KitchenWieldableSO output)
    {
        Destroy(KitchenWieldableHeld.gameObject);
        GameObject newItem = Instantiate(output._gameObject, SpawnPoint);
        newItem.transform.localPosition = Vector3.zero;
        Debug.Log(newItem.GetComponent<KitchenWieldable>());
        KitchenWieldableHeld = newItem.GetComponent<KitchenWieldable>();
        Debug.Log(KitchenWieldableHeld);
    }
}
