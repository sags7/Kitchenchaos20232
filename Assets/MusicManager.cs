using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    public static MusicManager Instance { get; private set; }
    public float MusicVolume { get; private set; }
    private float _cycleVolumeStep = 0.1f;
    private const string PREFS_MUSIC_VOLUME = "MusicVolume";


    private void Awake()
    {
        MusicVolume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, 1f);
        Instance = this;
    }
    public void CycleVolume()
    {
        MusicVolume += _cycleVolumeStep;
        if (MusicVolume > 1f + _cycleVolumeStep) MusicVolume = 0f;
        _musicAudioSource.volume = MusicVolume;
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, MusicVolume);
        PlayerPrefs.Save();
    }
}
