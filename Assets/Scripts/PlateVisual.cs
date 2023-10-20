using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [SerializeField] private Plate _plate;
    private List<GameObject> _visualItems;

    void Start()
    {
        _visualItems = new List<GameObject>();
        _plate.OnHeldItemsChange += OnHeldItemsChangedAction;
    }

    private void OnHeldItemsChangedAction(object sender, Plate.OnHeldItemsChangeEventArgs e)
    {
        float centerOffsetZ = 0.3f;
        float centerOffsetY = 0.1f;
        float scaleFactor = 0.05f * e.itemsList.Count;

        foreach (GameObject go in _visualItems) Destroy(go);
        _visualItems.Clear();
        foreach (KitchenWieldableSO item in e.itemsList)
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
