using UnityEngine;
using UnityEngine.UI;

public class PlateCanvasIcon : MonoBehaviour
{
    [SerializeField] private PlateUI _plateUI;
    [SerializeField] private Image _iconImage;

    public void SetSprite(Sprite sprite)
    {
        _iconImage.sprite = sprite;
    }
}
