using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BaseCounter _progressCounter;
    private const string _CUT = "Cut";
    private void Start()
    {
        _progressCounter.GetComponent<CuttingCounter>().OnProgressChange += OnProgressChangeAction;
    }
    private void OnProgressChangeAction(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        if (e.progress != 0)
        {
            _animator.SetTrigger(_CUT);
        }
    }
}
