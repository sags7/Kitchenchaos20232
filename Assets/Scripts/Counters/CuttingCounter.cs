using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public delegate void MyDelegate<T>(T arg);                  //just used my on delegate here to practice creating them
    public static event MyDelegate<int> OnAnyCuttingProgress;   //just used my on delegate here to practice creating them instead of using EventHandler, Func, Action....
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    [SerializeField] private CuttingRecipeSO[] _availableRecipesArr;
    private int _progress;

    new public static void ClearStaticData()
    {
        OnAnyCuttingProgress = null;
    }
    public override void Interacted(IKitchenWieldableParent player)
    {
        PopulatePlateOrSwap(player);
        _progress = 0;
        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progress = _progress });
    }
    public override void AlternativeInteracted(IKitchenWieldableParent player)
    {
        if (KitchenWieldableHeld && !player.KitchenWieldableHeld)
        {
            try { AttemptRecipes(_availableRecipesArr); }
            catch { Debug.Log("Counter has no recipes assigned!"); }
        }
    }

    internal void AttemptRecipes(CuttingRecipeSO[] availableRecipesArr)
    {
        foreach (CuttingRecipeSO recipe in availableRecipesArr)
        {
            if (KitchenWieldableHeld._kitchenWieldableSO == recipe.input)
            {
                _progress++;
                OnAnyCuttingProgress?.Invoke(3);//this event uses my own Delegate just to practice, should change to EventHandler at some point.
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progress = (float)_progress / recipe.cuttingNeeded });
                if (_progress >= recipe.cuttingNeeded)
                {
                    TransmuteTo(recipe.output);
                    _progress = 0;
                }
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
