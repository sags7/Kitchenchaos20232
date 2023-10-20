using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    [SerializeField] private CuttingRecipeSO[] _availableRecipesArr;
    private int _progress;

    public override void Interacted(IKitchenWieldableParent player)
    {
        PopulatePlateOrSwap(player);
        _progress = 0;
        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progress = _progress });
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

    internal void AttemptRecipes(CuttingRecipeSO[] availableRecipesArr)
    {
        foreach (CuttingRecipeSO recipe in availableRecipesArr)
        {
            if (KitchenWieldableHeld._kitchenWieldableSO == recipe.inputs[0])
            {
                _progress++;
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progress = (float)_progress / recipe.cuttingNeeded });
                if (_progress >= recipe.cuttingNeeded)
                {
                    TransmuteTo(recipe.output);
                    _progress = 0;
                }
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
