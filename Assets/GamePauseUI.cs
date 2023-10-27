using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.OnGamePaused += OnGamePausedAction;
        GameManager.Instance.OnGameUnpaused += OnGameUnPausedAction;

        gameObject.SetActive(false);
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
