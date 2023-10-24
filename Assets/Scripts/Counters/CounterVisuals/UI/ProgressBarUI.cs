using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] protected BaseCounter _progressCounter;
    [SerializeField] protected Image _barImage;

    //Make Derived Classes subscribe to the required events!!
    protected virtual void Update()
    {
        if (!_progressCounter.KitchenWieldableHeld) Hide();
    }
    protected virtual void OnProgressChangeAction(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        if (e.progress != 0)
        {
            Show();
        }
        else Hide();
        _barImage.fillAmount = e.progress;
    }
    protected void Show() => gameObject.SetActive(true);
    protected void Hide() => gameObject.SetActive(false);
}
