using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resumeButton;
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
