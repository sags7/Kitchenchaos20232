using System;
using UnityEngine;

public class FryingCounter : BaseCounter
{
    public event EventHandler<OnCuttingProgressChangeEventArgs> OnCuttingProgressChange;
    public class OnCuttingProgressChangeEventArgs : EventArgs { public float fryingProgress; }
    [SerializeField] private FryingRecipeSO[] _availableRecipesArr;
    private int _fryingProgress;

    public override void Interacted(IKitchenWieldableParent player)
    {
        SwapWieldablesWith(player);
        _fryingProgress = 0;
        OnCuttingProgressChange?.Invoke(this, new OnCuttingProgressChangeEventArgs { fryingProgress = _fryingProgress });
    }
    public override void AlternativeInteracted(IKitchenWieldableParent player)
    {
        if (KitchenWieldableHeld)
        {
            try { AttemptRecipes(_availableRecipesArr); }
            catch { Debug.Log("Counter has no recipes assigned!"); }
        }
        else Debug.Log("No Ingredients on Counter");
    }

    internal void AttemptRecipes(FryingRecipeSO[] availableRecipesArr)
    {
        foreach (FryingRecipeSO recipe in availableRecipesArr)
        {
            if (KitchenWieldableHeld._kitchenWieldableSO == recipe.inputs[0])
            {
                _fryingProgress++;
                OnCuttingProgressChange?.Invoke(this, new OnCuttingProgressChangeEventArgs { fryingProgress = (float)_fryingProgress / recipe.FryingNeeded });
                if (_fryingProgress >= recipe.FryingNeeded)
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
        KitchenWieldableHeld = newItem.GetComponent<KitchenWieldable>();
    }
}
