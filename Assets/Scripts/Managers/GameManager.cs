using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _mainCanvas;
    public static GameManager Instance { get; private set; }
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public bool IsGamePlaying { get => _state == State.GamePlaying; }
    public bool IsCountingDown { get => _state == State.CountdownToStart; }
    public bool IsGameOver { get => _state == State.GameOver; }
    public State _state;
    private bool _isWaitingToStart = true;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 300f;
    private bool _isGamePaused = false;


    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private void Start()
    {
        _mainCanvas.gameObject.SetActive(true);
        _state = State.WaitingToStart;
        _gamePlayingTimer = _gamePlayingTimerMax;

        GameInput.Instance.OnGamePaused += TogglePauseGame;
        GameInput.Instance.OnInteract += OnInteractAction;
    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        _isWaitingToStart = false;
        GameInput.Instance.OnInteract -= OnInteractAction;
    }

    public void TogglePauseGame(object sender, EventArgs e)
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused == true)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                if (_isWaitingToStart == false)
                    _state = State.CountdownToStart;
                break;
            case State.CountdownToStart:
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0f)
                {
                    _state = State.GamePlaying;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer < 0f)
                {
                    _state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                Debug.Log(_state);
                break;
        }
    }

    public float GetCountdownToStartTimer() => _countdownToStartTimer;
    public float GetGamePlayingTimerNormalized() => 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
}
