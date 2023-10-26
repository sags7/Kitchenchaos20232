using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float _footStepTimer;
    [SerializeField] private float _footStepTimerInterval = 0.1f;

    private void Start()
    {
        if (TryGetComponent(out Player player)) _player = player;
    }

    private void Update()
    {
        if (_player.IsWalking())
        {
            if (_footStepTimer < _footStepTimerInterval)
            {
                _footStepTimer += Time.deltaTime;
            }
            else
            {
                _footStepTimer = 0;
                SoundManager.Instance.PlayFootstepSound();
            }
        }
    }


}
