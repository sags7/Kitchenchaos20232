using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenuUI : MonoBehaviour
{
    public static SoundMenuUI Instance { get; private set; }
    [SerializeField] private Button _effectsButton;
    [SerializeField] private TextMeshProUGUI _effectsButtonText;
    [SerializeField] private Button _musicButton;
    [SerializeField] private TextMeshProUGUI _musicButtonText;
    [SerializeField] private Button _backButton;


    private void Awake()
    {
        Instance = this;

        _effectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.CycleVolume();
            UpdateVisual();
        });
        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.CycleVolume();
            UpdateVisual();
        });
        _backButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += OnGameUnpausedAction;
        UpdateVisual();
        Hide();
    }

    private void OnGameUnpausedAction(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        _effectsButtonText.text = "Effects Volume: " + Mathf.RoundToInt(SoundManager.Instance.EffectsVolume * 10);
        _musicButtonText.text = "Effects Volume: " + Mathf.RoundToInt(MusicManager.Instance.MusicVolume * 10);
    }

    internal void Show()
    {
        transform.gameObject.SetActive(true);
    }
    internal void Hide()
    {
        transform.gameObject.SetActive(false);
    }
}
