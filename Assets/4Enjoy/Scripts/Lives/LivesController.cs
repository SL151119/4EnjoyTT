using UnityEngine;
using static LivesPopupView;

public class LivesController : MonoBehaviour
{
    [SerializeField] private LivesView _livesView;
    [SerializeField] private LivesPopupView _livesPopupView;

    private LivesPopupState _state;

    private Lives _lives;

    private void OnEnable()
    {
        _livesView.OpenLivesPopupButton.Click += OnOpenLivesPopupButtonClick;
        _livesPopupView.CloseLivesPopupButton.Click += OnCloseLivesPopupButtonClick;
        _livesPopupView.UseLifeButton.Click += OnUseLifeButtonClick;
        _livesPopupView.RefillLivesButton.Click += OnRefillLivesButtonClick;
    }

    private void OnDisable()
    {
        _livesView.OpenLivesPopupButton.Click -= OnOpenLivesPopupButtonClick;
        _livesPopupView.CloseLivesPopupButton.Click -= OnCloseLivesPopupButtonClick;
        _livesPopupView.UseLifeButton.Click -= OnUseLifeButtonClick;
        _livesPopupView.RefillLivesButton.Click -= OnRefillLivesButtonClick;
    }

    public void Initialize(Lives lives)
    {
        _lives = lives;
    }

    private void OnOpenLivesPopupButtonClick()
    {
        _livesPopupView.PlayShowAnimation();
        UpdateLivesPopupState();
    }

    private void OnCloseLivesPopupButtonClick()
    {
        UpdateLivesPopupState();
        _livesPopupView.PlayHideAnimation();
    }

    private void OnUseLifeButtonClick()
    {
        _lives.UseLife();
        UpdateLivesPopupState();
    }

    private void OnRefillLivesButtonClick()
    {
        _lives.RefillLives();
        UpdateLivesPopupState();
    }

    private void UpdateLivesPopupState()
    {
        int currentLives = _lives.CurrentLives;
        int maxLives = _lives.MaxLives;

        if (currentLives == 0)
            _state = LivesPopupState.ZeroLives;
        else if (currentLives == maxLives)
            _state = LivesPopupState.MaxLives;
        else
            _state = LivesPopupState.Default;

        _livesPopupView.ChangeState(_state);
    }
}
