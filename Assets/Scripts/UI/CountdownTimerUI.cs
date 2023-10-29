using System;
using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    private const string TRIGGER_NUMBER_POPUP = "NumberPopUp";
    [SerializeField] private TextMeshProUGUI _countdownText;
    private Animator _animator;

    private int _previousCountdownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChangedAction;
        gameObject.SetActive(false);
    }

    private void OnGameStateChangedAction(object sender, EventArgs args)
    {
        if (GameManager.Instance.IsCountingDown) gameObject.SetActive(true);
        else gameObject.SetActive(false);

        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        _countdownText.text = countDownNumber.ToString();

        if (_previousCountdownNumber != countDownNumber)
        {
            _previousCountdownNumber = countDownNumber;
            _animator.SetTrigger(TRIGGER_NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }
}

