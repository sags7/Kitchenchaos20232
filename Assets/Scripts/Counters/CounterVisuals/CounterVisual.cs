using UnityEngine;

public class CounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _thisCounter;
    [SerializeField] private GameObject[] _visualArr;

    void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == _thisCounter) Show();
        else Hide();
    }

    private void Hide()
    {
        foreach (GameObject gameObject in _visualArr)
            gameObject.SetActive(false);
    }

    private void Show()
    {
        foreach (GameObject gameObject in _visualArr)
            gameObject.SetActive(true);
    }
}
