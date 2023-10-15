using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectedContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _thisCounter;
    [SerializeField] private GameObject[] _visualArr;
    private const string OPEN_CLOSE = "OpenClose";

    void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
        GetComponentInParent<ContainerCounter>().OnSpawnedItem += OnSpawnedItemAction;
    }

    private void OnSpawnedItemAction(object sender, EventArgs e)
    {
        GetComponent<Animator>().SetTrigger(OPEN_CLOSE);
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
