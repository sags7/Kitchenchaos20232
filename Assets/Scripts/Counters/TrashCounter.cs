using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interacted(IKitchenWieldableParent player)
    {
        SwapWieldablesWith(player);
        KitchenWieldableHeld?.DestroySelf();
    }
}
