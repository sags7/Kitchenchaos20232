using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenWieldable
{
    [SerializeField] private List<KitchenWieldableSO> _acceptableItems;
    private List<KitchenWieldableSO> _heldItems;

    private void Start()
    {
        _heldItems = new List<KitchenWieldableSO>();
    }
    public bool TryPutIntoPlate(KitchenWieldable kitchenWieldable)
    {
        if (_acceptableItems.Contains(kitchenWieldable._kitchenWieldableSO))
        {
            _heldItems.Add(kitchenWieldable._kitchenWieldableSO);
            kitchenWieldable.DestroySelf();
            return true;
        }
        else return false;
    }


}
