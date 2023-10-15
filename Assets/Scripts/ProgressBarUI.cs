using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private BaseCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _barImage.fillAmount = 0f;
        _cuttingCounter.GetComponent<CuttingCounter>().OnCuttingProgressChange += OnCuttingProgressChangeAction;
    }
    private void Update()
    {
        if (!_cuttingCounter.KitchenWieldableHeld) Hide();
    }
    private void OnCuttingProgressChangeAction(object sender, CuttingCounter.OnCuttingProgressChangeEventArgs e)
    {
        _barImage.fillAmount = e.cuttingProgress;
        Show();
    }
    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
