using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _soundButton;
    private void Start()
    {
        GameManager.Instance.OnGamePaused += OnGamePausedAction;
        GameManager.Instance.OnGameUnpaused += OnGameUnPausedAction;

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() => SceneLoader.Load(SceneLoader.Scene.MainMenuScene));
        _resumeButton.onClick.AddListener(() => GameManager.Instance.TogglePauseGame(this, EventArgs.Empty));
        _soundButton.onClick.AddListener(() => SoundMenuUI.Instance.Show());
    }

    private void OnGameUnPausedAction(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void OnGamePausedAction(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
