using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenWieldable
{
    public event EventHandler<OnHeldItemsChangeEventArgs> OnHeldItemsChange;
    public class OnHeldItemsChangeEventArgs : EventArgs { public List<KitchenWieldableSO> itemsList; }
    [SerializeField] private List<KitchenWieldableSO> _acceptableItems;
    private List<KitchenWieldableSO> _heldItems;
    public const int _MAX_ITEMS = 10;

    private void Start()
    {
        _heldItems = new List<KitchenWieldableSO>();
    }
    public bool TryPutIntoPlate(KitchenWieldable kitchenWieldable)
    {
        if (_acceptableItems.Contains(kitchenWieldable._kitchenWieldableSO) && _heldItems.Count < _MAX_ITEMS)
        {
            _heldItems.Add(kitchenWieldable._kitchenWieldableSO);
            OnHeldItemsChange?.Invoke(this, new OnHeldItemsChangeEventArgs { itemsList = _heldItems });
            kitchenWieldable.DestroySelf();
            return true;
        }
        else return false;
    }
}
