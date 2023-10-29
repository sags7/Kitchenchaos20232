using System;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefSO _audioClipRefSo;
    private float _effectVolumeMultiplier = 0.2f;
    private float _cycleVolumeStep = 0.1f;
    private const string PREFS_EFFECTS_VOLUME = "EffectsVolume";

    public float EffectsVolume { get; private set; }

    private void Awake()
    {
        Instance = this;
        EffectsVolume = PlayerPrefs.GetFloat(PREFS_EFFECTS_VOLUME, 1f);
    }

    public void CycleVolume()
    {
        EffectsVolume += _cycleVolumeStep;
        if (EffectsVolume > 1f + _cycleVolumeStep) EffectsVolume = 0f;
        PlayerPrefs.SetFloat(PREFS_EFFECTS_VOLUME, EffectsVolume);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderFail += OnOrderFailAction;
        DeliveryManager.Instance.OnOrderSuccess += OnOrderSuccessAction;
        CuttingCounter.OnAnyCuttingProgress += OnAnyCuttingProgressAction;
        BaseCounter.OnPlayerPickedSomething += OnPickedSomethingAction;
        BaseCounter.OnCounterPickedSomething += OnCounterPickedSomethingAction;
        TrashCounter.OnAnyObjectTrashed += Trash_OnAnyObjectTrashedAction;
    }


    public void PlayFootstepSound() =>
        PlaySound(_audioClipRefSo._footstep, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);
    public void PlayCountdownSound() =>
        PlaySound(_audioClipRefSo._warning, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);

    private void Trash_OnAnyObjectTrashedAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._trash, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);

    private void OnCounterPickedSomethingAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._objectDrop, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);

    private void OnPickedSomethingAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._objectPickup, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);

    private void OnAnyCuttingProgressAction(int number) =>
        PlaySound(_audioClipRefSo._chop, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);    //I just made the event use my own delegate to practice, should change to EventHandler later.   

    private void OnOrderSuccessAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._deliverySuccess, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);

    private void OnOrderFailAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._deliveryFail, Camera.main.transform.position, EffectsVolume * _effectVolumeMultiplier);

    private void PlaySound(AudioClip[] clip, Vector3 point, float volume = 1f) =>
        AudioSource.PlayClipAtPoint(clip[UnityEngine.Random.Range(0, clip.Length)], point, volume);
}
