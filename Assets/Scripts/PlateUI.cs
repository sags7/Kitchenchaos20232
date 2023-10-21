using UnityEngine;

public class PlateUI : MonoBehaviour
{
    [SerializeField] private Plate _plate;
    [SerializeField] private Transform _iconTemaplate;

    private void Start()
    {
        _plate.OnHeldItemsChange += OnHeldItemsChangedAction;
    }

    private void OnHeldItemsChangedAction(object sender, Plate.OnHeldItemsChangeEventArgs e)
    {
        foreach (Transform child in transform)
            if (child != _iconTemaplate) Destroy(child.gameObject);
        foreach (KitchenWieldableSO item in e.itemsList)
        {
            Transform newIcon = Instantiate(_iconTemaplate, transform);
            newIcon.gameObject.SetActive(true);
            newIcon.GetComponent<PlateCanvasIcon>().SetSprite(item._sprite);
        }
    }
}
