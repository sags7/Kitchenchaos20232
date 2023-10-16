using UnityEngine;
using UnityEngine.UI;

public class FryingProgressBarUI : MonoBehaviour
{
    [SerializeField] private BaseCounter _progressCounter;
    [SerializeField] private Image _barImage;
    private const string _CUT = "Cut";

    private void Start()
    {
        _barImage.fillAmount = 0f;
        _progressCounter.GetComponent<FryingCounter>().OnProgressChange += OnProgressChangeAction;
    }
    private void Update()
    {
        if (!_progressCounter.KitchenWieldableHeld) Hide();
    }
    private void OnProgressChangeAction(object sender, FryingCounter.OnFryingProgressChangeEventArgs e)
    {
        if (e.progress != 0)
        {
            Show();
        }
        else Hide();
        _barImage.fillAmount = e.progress;
    }
    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
