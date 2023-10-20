using UnityEngine;

public interface IKitchenWieldableParent
{
    public Transform SpawnPoint { get; set; }
    public KitchenWieldable KitchenWieldableHeld { get; set; }
    public void SwapWieldablesWith(IKitchenWieldableParent newParent);
    public bool TryPopulatePlate(IKitchenWieldableParent player);

    public static bool TryIsHolding<T>(KitchenWieldable kitchenWieldable, out T t) where T : KitchenWieldable //ED: this last part is called a Type Class Constraint
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

}
