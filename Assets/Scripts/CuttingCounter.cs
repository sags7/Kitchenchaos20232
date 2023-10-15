using UnityEngine;

public class CuttingCounter : BaseCounter
{
    // public event EventHandler OnSpawnedItem; //Unimplemented Event
    [SerializeField] private RecipeSO[] _availableRecipesArr;
    private int _cuttingProgress;

    public override void Interacted(IKitchenWieldableParent player)
    {
        TransferWieldableTo(player);
        _cuttingProgress = 0;
    }
    public override void AlternativeInteracted(Player player)
    {
        if (KitchenWieldableHeld)
        {
            try { AttemptRecipes(_availableRecipesArr); }
            catch { Debug.Log("Counter has no recipes assigned!"); }
        }
        else Debug.Log("No Ingredients on Counter");
    }

    internal void AttemptRecipes(RecipeSO[] availableRecipesArr)
    {
        foreach (RecipeSO recipe in availableRecipesArr)
        {
            if (KitchenWieldableHeld._kitchenWieldableSO == recipe.inputs[0])
            {
                _cuttingProgress++;
                if (_cuttingProgress >= recipe.cuttingNeeded)
                    TransmuteTo(recipe.output);
                //CURRENT IMPLEMENTATION ONLY WORKS WITH ONE INPUT INGREDIENT AND IS HARDCODED TO BE THE FIRST ON THE ARRAY!!!
            }
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
