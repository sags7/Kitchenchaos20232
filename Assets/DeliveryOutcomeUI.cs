using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DeliveryOutcomeUI : MonoBehaviour
{
    [SerializeField] private Transform _successCard;
    [SerializeField] private Transform _failureCard;
    private int _flashTimer = 1;


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
        GameManager.Instance.StartCoroutine(Flash(_successCard));
    }

    private void OnOrderFailAction(object sender, EventArgs e)
    {
        GameManager.Instance.StartCoroutine(Flash(_failureCard));
    }

    private IEnumerator Flash(Transform deliveryCard)
    {
        Show(deliveryCard);
        yield return new WaitForSeconds(_flashTimer);
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
