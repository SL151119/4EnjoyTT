using TMPro;
using UnityEngine;

public class LivesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerValue;
    [SerializeField] private TextMeshProUGUI _livesValue;

    [SerializeField] private CustomButton _openLivesPopupButton;

    public CustomButton OpenLivesPopupButton => _openLivesPopupButton;

    private Lives _lives;

    public void Initialize(Lives lives)
    {
        _lives = lives;

        UpdateLivesValue(_lives.GetCurrentLives());

        _lives.TimerValueChanged += UpdateTimerValue;
        _lives.LivesChanged += UpdateLivesValue;
    }

    private void OnDestroy() 
        => _lives.LivesChanged -= UpdateLivesValue;

    private void UpdateTimerValue(string timerValue) 
        => _timerValue.text = timerValue;

    private void UpdateLivesValue(int livesValue) 
        => _livesValue.text = livesValue.ToString();
}