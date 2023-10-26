using UnityEngine;
using UnityEngine.UI;

public class IndividualIngredientIconUI : MonoBehaviour
{
    [SerializeField] private Image _imageComponent;

    private void Start()
    {
        if (TryGetComponent(out Image a)) _imageComponent = a;
    }
    public void SetSprite(Sprite sprite)
    {
        _imageComponent.sprite = sprite;
    }
}
