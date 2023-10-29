using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public static OptionsMenuUI Instance { get; private set; }
    [SerializeField] private Button _effectsButton;
    [SerializeField] private TextMeshProUGUI _effectsButtonText;
    [SerializeField] private Button _musicButton;
    [SerializeField] private TextMeshProUGUI _musicButtonText;
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private TextMeshProUGUI _moveUpButtonText;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private TextMeshProUGUI _moveDownButtonText;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private TextMeshProUGUI _moveLeftButtonText;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private TextMeshProUGUI _moveRightButtonText;
    [SerializeField] private Button _interactButton;
    [SerializeField] private TextMeshProUGUI _interactButtonText;
    [SerializeField] private Button _interactAlternativeButton;
    [SerializeField] private TextMeshProUGUI _interactAlternativeButtonText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TextMeshProUGUI _pauseButtonText;
    [SerializeField] private Button _backButton;
    [SerializeField] private Transform _pressToRebindKeyUI;
    private Action _callersShowFunction;

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

        _moveUpButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Up));
        _moveDownButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Down));
        _moveLeftButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Left));
        _moveRightButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Right));
        _interactButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Interact));
        _interactAlternativeButton.onClick.AddListener(() => RebindKey(GameInput.Binding.InteractAlternative));
        _pauseButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Pause));

        HidePressToRebindUI();
    }

    private void RebindKey(GameInput.Binding binding)
    {
        ShowPressToRebindUI();
        GameInput.Instance.RebindKey(binding, () => { HidePressToRebindUI(); UpdateVisual(); });
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
        _musicButtonText.text = "Music Volume: " + Mathf.RoundToInt(MusicManager.Instance.MusicVolume * 10);

        _moveUpButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Up);
        _moveDownButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Down);
        _moveLeftButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Left);
        _moveRightButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Right);
        _interactButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Interact);
        _interactAlternativeButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.InteractAlternative);
        _pauseButtonText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Pause);
    }

    internal void Show(Action callersShow)
    {
        transform.gameObject.SetActive(true);
        _backButton.Select();
        _callersShowFunction = callersShow;
    }
    internal void Hide()
    {
        transform.gameObject.SetActive(false);
        _callersShowFunction?.Invoke();
        _callersShowFunction = null;
    }

    private void ShowPressToRebindUI()
    {
        _pressToRebindKeyUI.gameObject.SetActive(true);
    }
    private void HidePressToRebindUI()
    {
        _pressToRebindKeyUI.gameObject.SetActive(false);
    }
}
