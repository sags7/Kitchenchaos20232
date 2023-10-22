using System;

public class DeliveryCounter : BaseCounter
{
    public event EventHandler<OnPlateDeliveredEventArgs> OnPlateDelivered;
    public class OnPlateDeliveredEventArgs : EventArgs
    {
        public Plate _plateDelivered;
    }
    public override void Interacted(IKitchenWieldableParent player)
    {
        if (player.KitchenWieldableHeld is Plate)
        {
            SwapWieldablesWith(player);
            AttemptDelivery(KitchenWieldableHeld as Plate);
        }
        else if (KitchenWieldableHeld && !player.KitchenWieldableHeld)
            SwapWieldablesWith(player);
    }

    private void AttemptDelivery(Plate plateDelivered) => OnPlateDelivered?.Invoke(this, new DeliveryCounter.OnPlateDeliveredEventArgs { _plateDelivered = plateDelivered });
}
