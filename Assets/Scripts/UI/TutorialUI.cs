using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAlternativeText;
    [SerializeField] private TextMeshProUGUI _pauseText;

    private void Start()
    {
        GameInput.Instance.OnInteract += Hide;
        UpdateVisual();
    }

    private void Hide(object sender, EventArgs args)
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        _moveUpText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Up);
        _moveDownText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Down);
        _moveLeftText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Left);
        _moveRightText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Move_Right);
        _interactText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Interact);
        _interactAlternativeText.text = GameInput.Instance.GetBindingName(GameInput.Binding.InteractAlternative);
        _pauseText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Pause);
    }
}
