using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenWieldableParent
{

    public static event EventHandler OnPlayerPickedSomething;
    public static event EventHandler OnCounterPickedSomething;
    private void Start()
    {
        TrySetupSpawnPoint();
    }

    public static void ClearStaticData()
    {
        OnPlayerPickedSomething = null;
        OnCounterPickedSomething = null;
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
        PopulatePlateOrSwap(player);
    }

    protected void PopulatePlateOrSwap(IKitchenWieldableParent player)
    {
        if (!TryPopulatePlate(player))
            if (!player.TryPopulatePlate(this))
                SwapWieldablesWith(player);
    }


    public virtual void AlternativeInteracted(IKitchenWieldableParent player)
    {
        Debug.Log("This counter doesn't have an alternative interaction");
    }

    //--------------------------IKitchenWieldable implementation-----------------------------------------
    [field: SerializeField] public KitchenWieldable KitchenWieldableHeld { get; set; }
    [field: SerializeField] public Transform SpawnPoint { get; set; }

    public void SwapWieldablesWith(IKitchenWieldableParent newParent)
    {
        KitchenWieldable playerItem = newParent.KitchenWieldableHeld ? newParent.KitchenWieldableHeld : null;
        KitchenWieldable counterItem = KitchenWieldableHeld ? KitchenWieldableHeld : null;
        if (playerItem) playerItem.Set_ParentHoldingMe(this);
        KitchenWieldableHeld = playerItem;
        if (counterItem) counterItem.Set_ParentHoldingMe(newParent);
        newParent.KitchenWieldableHeld = counterItem;

        if (newParent.KitchenWieldableHeld) OnPlayerPickedSomething?.Invoke(this, EventArgs.Empty);
        if (KitchenWieldableHeld) OnCounterPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public bool TryPopulatePlate(IKitchenWieldableParent otherParent)
    {
        if (IKitchenWieldableParent.TryIsHolding<Plate>(otherParent.KitchenWieldableHeld, out Plate otherParentsPlate)
        && KitchenWieldableHeld ? !IKitchenWieldableParent.TryIsHolding<Plate>(KitchenWieldableHeld, out _) : false)
        {
            //Debug.Log("otherParent is holding a plate AND there is something other than a plate in me (this)");
            return otherParentsPlate.TryPutIntoPlate(KitchenWieldableHeld) ? true : false;
        }
        else return false;
    }
}
