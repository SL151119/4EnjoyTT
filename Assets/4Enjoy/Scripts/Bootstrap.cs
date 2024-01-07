using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LivesView _livesView;
    [SerializeField] private LivesPopupView _livesPopupView;
    [SerializeField] private LivesController _livesController;
    [SerializeField] private DailyBonusView _dailyBonusView;
    [SerializeField] private DailyBonusController _dailyBonusController;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private Lives _lives;
    private DailyBonus _dailyBonus;

    private void Awake()
    {
        InitializeData();

        InitializeLives();

        InitializeDailyBonus();
    }

    private void InitializeData()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);

        LoadDataOrInit();
    }

    private void InitializeLives()
    {
        _lives = new Lives(_persistentData, _dataProvider);

        _livesController.Initialize(_lives);

        _livesView.Initialize(_lives);

        _livesPopupView.Initialize(_lives);
  
        _lives.StartGame();
    }

    private void InitializeDailyBonus()
    {
        _dailyBonus = new DailyBonus(_persistentData, _dataProvider);

        _dailyBonusController.Initialize(_dailyBonus);

        _dailyBonusView.Initialize(_dailyBonus);

        _dailyBonusController.StartGame();
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.GameData = new GameData();
    }
}
