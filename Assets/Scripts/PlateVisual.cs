using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [SerializeField] private Plate _plate;
    private List<GameObject> _visualItemsList;
    private List<GameObject> _completeDishVisualItemsList;


    void Start()
    {
        _completeDishVisualItemsList = new List<GameObject>();
        _visualItemsList = new List<GameObject>();
        _plate.OnHeldItemsChange += OnHeldItemsChangedAction;
        _plate.OnDishComplete += OnDishCompleteAction;
    }

    private void OnDishCompleteAction(object sender, Plate.OnDishCompleteEventArgs e)
    {
        ResetVisual();

        //instantiate all objects of the Recipes visual list
        foreach (GameObject item in e.CompleteDishVisualsList)
        {
            GameObject newItem = Instantiate(item, transform.up, Quaternion.identity, transform);
            newItem.transform.localPosition = Vector3.zero;
            _completeDishVisualItemsList.Add(newItem);
        }
    }

    private void OnHeldItemsChangedAction(object sender, Plate.OnHeldItemsChangeEventArgs e)
    {
        float centerOffsetZ = 0.3f;
        float centerOffsetY = 0.1f;
        float scaleFactor = 0.05f * e.itemsList.Count;

        ResetVisual();

        //instantiate every item on the items list i get from the EventCaller
        foreach (KitchenWieldableSO item in e.itemsList)
        {
            GameObject newItem = Instantiate(item._gameObject, transform.up, Quaternion.identity, transform);
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
            _visualItemsList.Add(newItem);
        }
        //If there is more than one item in the list, place them in the plate in a circular pattern.
        if (_visualItemsList.Count > 1)
        {
            foreach (GameObject gameObject in _visualItemsList)
            {
                float rotationAngle = 360 / _visualItemsList.Count * (_visualItemsList.IndexOf(gameObject) + 1);
                gameObject.transform.localPosition = transform.localPosition + new Vector3(0, centerOffsetY, centerOffsetZ);
                gameObject.transform.RotateAround(transform.position, Vector3.up, rotationAngle);
            }
        }
    }

    private void ResetVisual()
    {
        //Remove all visuals from the _CompleteDishVisualItems
        foreach (GameObject go in _completeDishVisualItemsList) Destroy(go);
        _completeDishVisualItemsList?.Clear();

        //remove all visuals for the _visualItemsList before redrawing it all
        foreach (GameObject go in _visualItemsList) Destroy(go);
        _visualItemsList?.Clear();
    }
}
