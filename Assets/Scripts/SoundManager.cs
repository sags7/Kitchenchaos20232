using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefSO _audioClipRefSo;
    private float _effectVolume = 0.2f;

    private void Awake()
    {
        Instance = this;
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


    public void PlayFootstepSound()
    {
        PlaySound(_audioClipRefSo._footstep, Camera.main.transform.position, _effectVolume);
    }
    private void Trash_OnAnyObjectTrashedAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._trash, Camera.main.transform.position, _effectVolume);

    private void OnCounterPickedSomethingAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._objectDrop, Camera.main.transform.position, _effectVolume);

    private void OnPickedSomethingAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._objectPickup, Camera.main.transform.position, _effectVolume);

    private void OnAnyCuttingProgressAction(int number) =>
        PlaySound(_audioClipRefSo._chop, Camera.main.transform.position, _effectVolume);    //I just made the event use my own delegate to practice, should change to EventHandler later.   

    private void OnOrderSuccessAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._deliverySuccess, Camera.main.transform.position, _effectVolume);

    private void OnOrderFailAction(object sender, EventArgs e) =>
        PlaySound(_audioClipRefSo._deliveryFail, Camera.main.transform.position, _effectVolume);

    private void PlaySound(AudioClip[] clip, Vector3 point, float volume = 1f) =>
        AudioSource.PlayClipAtPoint(clip[UnityEngine.Random.Range(0, clip.Length)], point, volume);
}
