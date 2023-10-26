using UnityEngine;

public class FryingCounterSound : MonoBehaviour
{
    [SerializeField] FryingCounter _fryingCounter;
    [SerializeField] AudioSource _sizzleSoundSource;
    private bool _isFrying = false;

    private void Start()
    {
        _fryingCounter.OnProgressChange += OnProgressChangeAction;
    }

    private void OnProgressChangeAction(object sender, IHasProgress.OnProgressChangeEventArgs args)
    {
        if (args.progress != 0 && _isFrying == true) return; //this return prevents the audio clip to be re-played on every progress change
        else if (args.progress != 0) _isFrying = true;
        else _isFrying = false;

        if (_isFrying == true) _sizzleSoundSource.Play();
        else _sizzleSoundSource.Pause();
    }
}
