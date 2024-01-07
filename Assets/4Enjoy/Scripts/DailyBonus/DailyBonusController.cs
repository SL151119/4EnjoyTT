using UnityEngine;

public class DailyBonusController : MonoBehaviour
{
    [SerializeField] private DailyBonusView _dailyBonusView;

    private DailyBonus _dailyBonus;

    private void OnEnable() 
        => _dailyBonusView.ClaimButton.Click += OnDailyBonusClaimButtonClick;

    private void OnDisable() 
        => _dailyBonusView.ClaimButton.Click -= OnDailyBonusClaimButtonClick;

    public void Initialize(DailyBonus dailyBonus)
    {
        _dailyBonus = dailyBonus;
    }

    public void StartGame()
    {
        if (_dailyBonus.IsNewDailyBonus())
        {
            _dailyBonus.GetDailyBonusCoins();

            _dailyBonusView.TogglePopupVisibility(true);
        }
    }

    private void OnDailyBonusClaimButtonClick()
    {
        _dailyBonusView.TogglePopupVisibility(false);

        _dailyBonus.SaveNewDailyBonus(); //I think we need save LastDailyBonusDate only when we claimed it,
                                         //otherwise in the same day DailyBonus menu will not open again
    }
}
