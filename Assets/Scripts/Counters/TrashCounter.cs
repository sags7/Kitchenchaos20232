using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;
    public override void Interacted(IKitchenWieldableParent player)
    {
        SwapWieldablesWith(player);
        KitchenWieldableHeld?.DestroySelf();
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
}
