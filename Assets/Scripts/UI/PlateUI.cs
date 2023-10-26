using UnityEngine;

public class PlateUI : MonoBehaviour
{
    [SerializeField] private Plate _plate;
    [SerializeField] private Transform _iconTemaplate;

    private void Start()
    {
    }

    private void Awake()
    {
        _plate.OnHeldItemsChange += OnHeldItemsChangedAction;
    }

    private void OnHeldItemsChangedAction(object sender, Plate.OnHeldItemsChangeEventArgs e)
    {
        //Cleans up the UI (except the Template)
        foreach (Transform child in transform)
            if (child != _iconTemaplate) Destroy(child.gameObject);

        //Instantiates and icon for each item received in e.itemsList
        foreach (KitchenWieldableSO item in e.itemsList)
        {
            Transform newIcon = Instantiate(_iconTemaplate, transform);
            newIcon.gameObject.SetActive(true);
            newIcon.GetComponent<PlateCanvasIcon>().SetSprite(item._sprite);
        }
    }
}
