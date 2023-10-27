using System;
using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChangedAction;
        gameObject.SetActive(false);
    }

    private void OnGameStateChangedAction(object sender, EventArgs args)
    {
        if (GameManager.Instance.IsCountingDown) gameObject.SetActive(true);
        else gameObject.SetActive(false);
        _countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
    }
}

