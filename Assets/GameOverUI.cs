using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ordersDeliveredNumber;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChangedAction;
        gameObject.SetActive(false);
    }

    private void OnGameStateChangedAction(object sender, EventArgs args)
    {
        if (GameManager.Instance.IsGameOver) gameObject.SetActive(true);
        else gameObject.SetActive(false);
        _ordersDeliveredNumber.text = DeliveryManager.Instance.SuccessfulDeliveries.ToString();
    }
}
