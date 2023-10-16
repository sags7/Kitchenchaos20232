using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenWieldableParent
{
    private void Start()
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
        SwapWieldablesWith(player);
    }
    public virtual void AlternativeInteracted(IKitchenWieldableParent player)
    {
        Debug.Log("This counter doesn't have an alternative interaction");
    }

    //--------------------------IKitchenWieldable implementation-----------------------------------------
    public KitchenWieldable KitchenWieldableHeld { get; set; }
    [field: SerializeField] public Transform SpawnPoint { get; set; }

    public void SwapWieldablesWith(IKitchenWieldableParent player)
    {
        KitchenWieldable playerItem = player.KitchenWieldableHeld ? player.KitchenWieldableHeld : null;
        KitchenWieldable counterItem = KitchenWieldableHeld ? KitchenWieldableHeld : null;
        if (playerItem) playerItem.Set_ParentHoldingMe(this);
        KitchenWieldableHeld = playerItem;
        if (counterItem) counterItem.Set_ParentHoldingMe(player);
        player.KitchenWieldableHeld = counterItem;
    }
}
