using UnityEngine;

public class KitchenWieldable : MonoBehaviour
{
    [SerializeField] public KitchenWieldableSO _kitchenWieldableSO;
    private IKitchenWieldableParent _parentHoldingMe;

    public KitchenWieldableSO GetKitchenWieldableSO() => _kitchenWieldableSO;
    public IKitchenWieldableParent Get_ParentHoldingMe() => _parentHoldingMe;

    public void Set_ParentHoldingMe(IKitchenWieldableParent newParent)
    {
        _parentHoldingMe = newParent;
        newParent.KitchenWieldableHeld = this;
        transform.parent = newParent.SpawnPoint;
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        try { _parentHoldingMe.KitchenWieldableHeld = null; }
        catch { }// Debug.Log("KitchenWieldable has no parent counter"); };
        Destroy(transform.gameObject);
    }

}
