using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;

    private void OnEnable() 
        => _button.Add(OnButtonClick);

    private void OnDisable() 
        => _button.Remove(OnButtonClick);

    public void ChangeButtonInteractableState(bool isActive)
    {
        _button.interactable = isActive;
    }

    private void OnButtonClick()
    {
        Click?.Invoke();
    }
}
