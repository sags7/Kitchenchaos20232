public class CuttingProgressBarUI : ProgressBarUI
{
    private void Start()
    {
        _barImage.fillAmount = 0f;
        _progressCounter.GetComponent<CuttingCounter>().OnProgressChange += OnProgressChangeAction;
    }
}
