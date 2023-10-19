using System;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenWieldableParent
{
    private void Start()
    {
        TrySetupSpawnPoint();
    }

    private void TrySetupSpawnPoint()
    {
        try
        {
            if (!SpawnPoint && transform.GetChild(0).name == "SpawnPoint")
            {
                SpawnPoint = transform.GetChild(0);
                Debug.Log("Forgot to setup counter SpawnPoint in inspector, setup automatically");
            }
        }
        catch { Debug.Log("Counter Spawn Point Set Incorrectly"); }
    }

    public virtual void Interacted(IKitchenWieldableParent player)
    {
        if (!TryPopulatePlate(player)) SwapWieldablesWith(player);
    }

    protected bool TryPopulatePlate(IKitchenWieldableParent player)
    {

        if (TryIsHolding<Plate>(player.KitchenWieldableHeld, out Plate playerPlate) && KitchenWieldableHeld ? !TryIsHolding<Plate>(KitchenWieldableHeld, out _) : false)
        {
            //Debug.Log("Player is holding a plate AND there is something other than a plate in the counter");
            playerPlate.PutIntoPlate(KitchenWieldableHeld);
            return true;
        }
        else if (TryIsHolding<Plate>(KitchenWieldableHeld, out Plate counterPlate) && player.KitchenWieldableHeld ? !TryIsHolding<Plate>(player.KitchenWieldableHeld, out _) : false)
        {
            //Debug.Log("There is a plate in the counter AND Player is holding something other than a plate");
            counterPlate.PutIntoPlate(player.KitchenWieldableHeld);
            return true;
        }
        else return false;

    }



    public static bool TryIsHolding<T>(KitchenWieldable kitchenWieldable, out T t) where T : KitchenWieldable //ED: this last part it called a Type Class Constraint
    {
        if (kitchenWieldable is T)
        {
            t = kitchenWieldable as T;
            return true;
        }
        else
        {
            t = null;
            return t; //Q: does an object cast to a bool depend on in existing or not? A: you can return an object of any type from a method that returns bool. This is because the compiler will automatically convert the object to a bool value before returning it.
        }
    }

    public virtual void AlternativeInteracted(IKitchenWieldableParent player)
    {
        Debug.Log("This counter doesn't have an alternative interaction");
    }

    //--------------------------IKitchenWieldable implementation-----------------------------------------
    [field: SerializeField] public KitchenWieldable KitchenWieldableHeld { get; set; }
    [field: SerializeField] public Transform SpawnPoint { get; set; }

    public void SwapWieldablesWith(IKitchenWieldableParent player)
    {
        KitchenWieldable playerItem = player.KitchenWieldableHeld ? player.KitchenWieldableHeld : null;
        KitchenWieldable counterItem = KitchenWieldableHeld ? KitchenWieldableHeld : null;
        if (playerItem) playerItem.Set_ParentHoldingMe(this);
        KitchenWieldableHeld = playerItem;
        if (counterItem) counterItem.Set_ParentHoldingMe(player);
        player.KitchenWieldableHeld = counterItem;
    }
}
