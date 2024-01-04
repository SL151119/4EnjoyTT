using TMPro;
using UnityEngine;

public class LivesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesValue;
    
    private Lives _lives;

    public void Initialize(Lives lives)
    {
        _lives = lives;

        UpdateLivesValue(_lives.GetCurrentLives());

        _lives.LivesChanged += UpdateLivesValue;
    }

    private void OnDestroy() => _lives.LivesChanged -= UpdateLivesValue;

    private void UpdateLivesValue(int livesValue) => _livesValue.text = livesValue.ToString();
}
