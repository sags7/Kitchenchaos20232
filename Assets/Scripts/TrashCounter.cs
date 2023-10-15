using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interacted(IKitchenWieldableParent player)
    {
        TransferWieldableTo(player);
        KitchenWieldableHeld.DestroySelf();
    }
}
