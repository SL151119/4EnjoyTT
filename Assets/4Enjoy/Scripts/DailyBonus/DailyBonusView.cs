using TMPro;
using UnityEngine;

public class DailyBonusView : MonoBehaviour
{
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private CustomButton _claimButton;
    [SerializeField] private TextMeshProUGUI _coinsValue;

    public CustomButton ClaimButton => _claimButton;

    private bool _currentDailyBonusStatus;

    private DailyBonus _dailyBonus;

    public void Initialize(DailyBonus dailyBonus)
    {
        _dailyBonus = dailyBonus;

        _dailyBonus.CoinsWasCalculated += UpdateCoinsValue;

    }

    private void OnDestroy()
        => _dailyBonus.CoinsWasCalculated -= UpdateCoinsValue;

    public void TogglePopupVisibility(bool shouldBeVisible)
    {
        if (shouldBeVisible == _currentDailyBonusStatus)
            return;

        if (shouldBeVisible)
            _animatorController.PlayAnimation(AnimatorController.AnimatorState.Show);

        else
            _animatorController.PlayAnimation(AnimatorController.AnimatorState.Hide);

        float clipLength = 0.75f; //It's not quite right, but I tried to find solution via info.clip.length

        this.UniversalWait(clipLength, () =>
            _currentDailyBonusStatus = shouldBeVisible
        );
    }

    private void UpdateCoinsValue(long coinsValue)
        => _coinsValue.text = $"Your daily bonus: {coinsValue}";
}
