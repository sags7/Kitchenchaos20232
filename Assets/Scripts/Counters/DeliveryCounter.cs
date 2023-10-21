using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interacted(IKitchenWieldableParent player)
    {
        if (player.KitchenWieldableHeld is Plate)
        {
            SwapWieldablesWith(player);
            KitchenWieldableHeld?.DestroySelf();
        }
    }
}
