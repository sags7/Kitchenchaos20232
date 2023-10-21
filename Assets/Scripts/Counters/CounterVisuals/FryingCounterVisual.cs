using UnityEngine;

public class FryingCounterVisual : MonoBehaviour
{
    [SerializeField] private FryingCounter _fryingCounter;
    [SerializeField] private GameObject _particles;
    [SerializeField] private GameObject _glow;

    private void Update()
    {
        if (_fryingCounter.GetProgress() != 0)
        {
            _particles.SetActive(true);
            _glow.SetActive(true);
        }
        else
        {
            _particles.SetActive(false);
            _glow.SetActive(false);
        }
    }
}
