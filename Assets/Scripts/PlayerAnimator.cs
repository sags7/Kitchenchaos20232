using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void Awake()
    {
        if (transform.TryGetComponent<Animator>(out Animator a)) _animator = a;
        if (transform.parent.TryGetComponent<Player>(out Player p)) _player = p;
    }

    private void Update()
    {
        if (_player) _animator.SetBool(IS_WALKING, _player.IsWalking());
    }
}
