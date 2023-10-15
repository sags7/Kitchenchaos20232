using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private BaseCounter _cuttingCounter;
    [SerializeField] private Image _barImage;
    [SerializeField] private Animator _animator;
    private const string _CUT = "Cut";

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
        if (e.cuttingProgress != 0)
        {
            _animator.SetTrigger(_CUT);
            Show();
        }
        else Hide();
        _barImage.fillAmount = e.cuttingProgress;
    }
    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
