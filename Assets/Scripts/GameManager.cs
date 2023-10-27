using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public bool IsGamePlaying { get => _state == State.GamePlaying; }
    public bool IsCountingDown { get => _state == State.CountdownToStart; }
    public bool IsGameOver { get => _state == State.GameOver; }
    public State _state;
    private float _waitingToStartTimer = 1f;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 20f;
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
        _state = State.WaitingToStart;
        _gamePlayingTimer = _gamePlayingTimerMax;

        GameInput.Instance.OnGamePaused += PauseGameToggle;
    }

    private void PauseGameToggle(object sender, EventArgs e)
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
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer < 0f)
                {
                    _state = State.CountdownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
