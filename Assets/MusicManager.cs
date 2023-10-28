using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    public static MusicManager Instance { get; private set; }
    public float MusicVolume { get; private set; }
    private float _cycleVolumeStep = 0.1f;


    private void Awake()
    {
        Instance = this;
    }
    public void CycleVolume()
    {
        MusicVolume += _cycleVolumeStep;
        if (MusicVolume > 1f) MusicVolume = 0f;
        _musicAudioSource.volume = MusicVolume;
    }
}
