using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenWieldableSO _kitchenWieldableSO;
    public event EventHandler OnSpawnedItem;

    public override void Interacted(IKitchenWieldableParent player)
    {
        if (!KitchenWieldableHeld && !player.KitchenWieldableHeld)
            SpawnItem(player);
        else PopulatePlateOrSwap(player);
    }

    private void SpawnItem(IKitchenWieldableParent parent)
    {
        GameObject newItem = Instantiate(_kitchenWieldableSO._gameObject, parent.SpawnPoint);
        newItem.transform.localPosition = Vector3.zero;
        KitchenWieldableHeld = newItem.GetComponent<KitchenWieldable>();
        SwapWieldablesWith(parent);

        OnSpawnedItem?.Invoke(this, EventArgs.Empty);
    }
}
