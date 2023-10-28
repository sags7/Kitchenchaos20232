using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ClearStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interacted(IKitchenWieldableParent player)
    {
        SwapWieldablesWith(player);
        KitchenWieldableHeld?.DestroySelf();
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
}
