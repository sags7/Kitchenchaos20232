using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenWieldable
{
    [SerializeField] private List<KitchenWieldableSO> _acceptableItems;
    private List<KitchenWieldableSO> _heldItems;
    private List<GameObject> _visualItems;
    public const int _MAX_ITEMS = 10;

    private void Start()
    {
        _visualItems = new List<GameObject>();
        _heldItems = new List<KitchenWieldableSO>();
    }
    public bool TryPutIntoPlate(KitchenWieldable kitchenWieldable)
    {
        if (_acceptableItems.Contains(kitchenWieldable._kitchenWieldableSO) && _heldItems.Count < _MAX_ITEMS)
        {
            _heldItems.Add(kitchenWieldable._kitchenWieldableSO);
            UpdateVisual();
            kitchenWieldable.DestroySelf();
            return true;
        }
        else return false;
    }

    private void UpdateVisual()
    {
        float centerOffsetZ = 0.3f;
        float centerOffsetY = 0.1f;
        float scaleFactor = 0.05f * _heldItems.Count;

        foreach (GameObject go in _visualItems) Destroy(go);
        _visualItems.Clear();
        foreach (KitchenWieldableSO item in _heldItems)
        {
            GameObject newItem = Instantiate(item._gameObject, transform.up, Quaternion.identity, transform);
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
            _visualItems.Add(newItem);
        }
        foreach (GameObject gameObject in _visualItems)
        {
            float rotationAngle = (360 / _visualItems.Count) * (_visualItems.IndexOf(gameObject) + 1);
            gameObject.transform.localPosition = transform.localPosition + new Vector3(0, centerOffsetY, centerOffsetZ);
            gameObject.transform.RotateAround(transform.position, Vector3.up, rotationAngle);
        }
    }
}
