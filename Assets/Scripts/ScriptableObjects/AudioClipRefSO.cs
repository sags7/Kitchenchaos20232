using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefSO : ScriptableObject
{
    [SerializeField] public AudioClip[] _chop;
    [SerializeField] public AudioClip[] _deliverySuccess;
    [SerializeField] public AudioClip[] _deliveryFail;
    [SerializeField] public AudioClip[] _objectDrop;
    [SerializeField] public AudioClip[] _objectPickup;
    [SerializeField] public AudioClip[] _panSizzle;
    [SerializeField] public AudioClip[] _trash;
    [SerializeField] public AudioClip[] _warning;
    [SerializeField] public AudioClip[] _footstep;
}
