using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesPopupView : MonoBehaviour
{
    public enum LivesPopupState
    {
        ZeroLives,
        Default,
        MaxLives
    }

    [SerializeField] private TextMeshProUGUI _timerPopupValue;
    [SerializeField] private TextMeshProUGUI _livesPopupValue;
    [SerializeField] private CustomButton _closeLivesPopupButton;
    [SerializeField] private CustomButton _useLifeButton;
    [SerializeField] private CustomButton _refillLivesButton;
    [SerializeField] private CustomButton _backgroundCloseLivesPopupButton;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private AnimatorController[] _animatorControllers;
    
    public CustomButton UseLifeButton => _useLifeButton;
    public CustomButton RefillLivesButton => _refillLivesButton;
    public CustomButton CloseLivesPopupButton => _closeLivesPopupButton;
    public CustomButton BackgroundCloseLivesPopupButton => _backgroundCloseLivesPopupButton;

    private bool _currentPopupStatus;

    private Lives _lives;

    private LivesPopupState _livesPopupState = LivesPopupState.Default;

    public void Initialize(Lives lives)
    {
        _lives = lives;

        UpdateLivesValue(_lives.CurrentLives);

        _lives.TimerValueChanged += UpdateTimerValue;
        _lives.LivesChanged += UpdateLivesValue;
    }

    private void OnDestroy() => 
        _lives.LivesChanged -= UpdateLivesValue;


    public void TogglePopupVisibility(bool shouldBeVisible)
    {
        if (shouldBeVisible == _currentPopupStatus) 
            return;

        if (shouldBeVisible)
            foreach (AnimatorController animController in _animatorControllers)
                animController.PlayAnimation(AnimatorController.AnimatorState.Show);
        else
            foreach (AnimatorController animController in _animatorControllers)
                animController.PlayAnimation(AnimatorController.AnimatorState.Hide);
                    
        bool isActive = shouldBeVisible;
        float clipLength = 0.75f; //It's not quite right, but I tried to find solution via info.clip.length

        this.UniversalWait(clipLength, () =>
            {
                _backgroundCloseLivesPopupButton.ChangeButtonInteractableState(isActive);
                _backgroundImage.raycastTarget = isActive;

                _currentPopupStatus = shouldBeVisible;
            });
    }

    public void ChangeState(LivesPopupState state)
    {
        if (_livesPopupState == state)
            return;
        
        switch(state)
        {
            default:
                _timerPopupValue.Activate();
                _useLifeButton.Activate();
                _refillLivesButton.Activate();
                break;
            case LivesPopupState.ZeroLives:
                _timerPopupValue.Activate();
                _useLifeButton.Deactivate();
                _refillLivesButton.Activate();
                break;
            case LivesPopupState.Default:
                _timerPopupValue.Activate();
                _useLifeButton.Activate();
                _refillLivesButton.Activate();
                break;
            case LivesPopupState.MaxLives:
                _timerPopupValue.Deactivate();
                _useLifeButton.Activate();
                _refillLivesButton.Deactivate();
                break;
        }

        _livesPopupState = state;
    }

    private void UpdateTimerValue(string timerValue) 
        => _timerPopupValue.text = timerValue;

    private void UpdateLivesValue(int livesValue) 
        => _livesPopupValue.text = livesValue.ToString();
}
