using UnityEngine;

public interface IKitchenWieldableParent
{
    public Transform SpawnPoint { get; set; }
    public KitchenWieldable KitchenWieldableHeld { get; set; }
    public void TransferWieldableTo(IKitchenWieldableParent newParent);
}