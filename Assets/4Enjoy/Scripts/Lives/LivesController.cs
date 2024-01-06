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
        _livesPopupView.BackgroundCloseLivesPopupButton.Click += OnCloseLivesPopupButtonClick;
        _livesPopupView.UseLifeButton.Click += OnUseLifeButtonClick;
        _livesPopupView.RefillLivesButton.Click += OnRefillLivesButtonClick;
    }

    private void OnDisable()
    {
        _livesView.OpenLivesPopupButton.Click -= OnOpenLivesPopupButtonClick;
        _livesPopupView.CloseLivesPopupButton.Click -= OnCloseLivesPopupButtonClick;
        _livesPopupView.BackgroundCloseLivesPopupButton.Click -= OnCloseLivesPopupButtonClick;
        _livesPopupView.UseLifeButton.Click -= OnUseLifeButtonClick;
        _livesPopupView.RefillLivesButton.Click -= OnRefillLivesButtonClick;
    }

    public void Initialize(Lives lives)
    {
        _lives = lives;

        _lives.LivesChanged += UpdateLivesPopupState;

        UpdateLivesPopupState(_lives.CurrentLives);
    }

    private void OnDestroy() =>
        _lives.LivesChanged -= UpdateLivesPopupState;

    private void OnOpenLivesPopupButtonClick()
    {
        _livesPopupView.TogglePopupVisibility(true);
    }

    private void OnCloseLivesPopupButtonClick()
    {
        _livesPopupView.TogglePopupVisibility(false);
    }

    private void OnUseLifeButtonClick()
    {
        _lives.UseLife();
    }

    private void OnRefillLivesButtonClick()
    {
        _lives.RefillLives();
    }

    private void UpdateLivesPopupState(int lives)
    {
        int maxLives = _lives.MaxLives;

        if (lives == 0)
            _state = LivesPopupState.ZeroLives;
        else if (lives == maxLives)
            _state = LivesPopupState.MaxLives;
        else
            _state = LivesPopupState.Default;

        _livesPopupView.ChangeState(_state);
    }
}
