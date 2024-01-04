using System;
using UnityEngine;
using UnityEngine.UI;

public class LivesPopupButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;

    private void OnEnable() => _button.Add(OnButtonClick);

    private void OnDisable() => _button.Remove(OnButtonClick);

    private void OnButtonClick()
    {
        Click?.Invoke();
    }
}
