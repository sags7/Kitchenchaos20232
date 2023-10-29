using System;
using System.Threading.Tasks;
using UnityEngine;

public class DeliveryOutcomeUI : MonoBehaviour
{
    [SerializeField] private Transform _successCard;
    [SerializeField] private Transform _failureCard;
    private int _flashTimer = 1000;


    private void Awake()
    {
    }

    private void Start()
    {
        Hide(_successCard);
        Hide(_failureCard);

        DeliveryManager.Instance.OnOrderFail += OnOrderFailAction;
        DeliveryManager.Instance.OnOrderSuccess += OnOrderSuccessAction;
    }

    private void OnOrderSuccessAction(object sender, EventArgs e)
    {
        Flash(_successCard);
    }

    private void OnOrderFailAction(object sender, EventArgs e)
    {
        Flash(_failureCard);
    }

    private async void Flash(Transform deliveryCard)
    {
        Show(deliveryCard);
        await Task.Delay(_flashTimer);
        Hide(deliveryCard);
    }

    private void Hide(Transform transform)
    {
        transform.gameObject.SetActive(false);
    }

    private void Show(Transform transform)
    {
        transform.gameObject.SetActive(true);
    }
}
