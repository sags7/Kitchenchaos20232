using System;
using UnityEngine;

public class FryingCounter : BaseCounter
{
    public event EventHandler<OnFryingProgressChangeEventArgs> OnProgressChange;
    public class OnFryingProgressChangeEventArgs : EventArgs { public float progress; }
    [SerializeField] private FryingRecipeSO[] _availableRecipesArr;
    private float _progress;

    public override void Interacted(IKitchenWieldableParent player)
    {
        SwapWieldablesWith(player);
        _progress = 0;
        OnProgressChange?.Invoke(this, new OnFryingProgressChangeEventArgs { progress = _progress });
    }
    public override void AlternativeInteracted(IKitchenWieldableParent player)
    {
        if (KitchenWieldableHeld && _progress == 0)
        {
            try { AttemptRecipes(_availableRecipesArr); }
            catch { Debug.Log("Counter has no recipes assigned!"); }
        }
        else if (KitchenWieldableHeld && _progress != 0)
        {
            _progress = 0;
            OnProgressChange?.Invoke(this, new OnFryingProgressChangeEventArgs { progress = _progress });
        }
        else Debug.Log("No Ingredients on Counter");
    }

    private void Update()
    {
        if (_progress != 0) AttemptRecipes(_availableRecipesArr);
    }

    internal void AttemptRecipes(FryingRecipeSO[] availableRecipesArr)
    {
        foreach (FryingRecipeSO recipe in availableRecipesArr)
        {
            if (KitchenWieldableHeld._kitchenWieldableSO == recipe.inputs[0])
            {
                _progress += Time.deltaTime;
                OnProgressChange?.Invoke(this, new OnFryingProgressChangeEventArgs { progress = (float)_progress / recipe.FryingNeeded });
                if (_progress >= recipe.FryingNeeded)
                {
                    TransmuteTo(recipe.output);
                    _progress = 1 * Time.deltaTime;
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

    public float GetProgress() => _progress;
}
