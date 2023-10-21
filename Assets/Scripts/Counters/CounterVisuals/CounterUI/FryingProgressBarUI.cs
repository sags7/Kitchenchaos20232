public class FryingProgressBarUI : ProgressBarUI
{
    private void Start()
    {
        _barImage.fillAmount = 0f;
        _progressCounter.GetComponent<FryingCounter>().OnProgressChange += OnProgressChangeAction;
    }
}
