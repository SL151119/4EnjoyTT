using TMPro;
using UnityEngine;

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
    [SerializeField] private CustomButton _useLifeButton;
    [SerializeField] private CustomButton _refillLivesButton;
    [SerializeField] private CustomButton _closeLivesPopupButton;
    [SerializeField] private AnimatorController _animatorController;
    
    public CustomButton UseLifeButton => _useLifeButton;
    public CustomButton RefillLivesButton => _refillLivesButton;
    public CustomButton CloseLivesPopupButton => _closeLivesPopupButton;

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

    public void PlayShowAnimation()
    {
        _animatorController.PlayAnimation(AnimatorController.AnimatorState.Show);
    }

    public void PlayHideAnimation()
    {
        _animatorController.PlayAnimation(AnimatorController.AnimatorState.Hide);
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
