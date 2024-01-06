using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LivesView _livesView;
    [SerializeField] private LivesPopupView _livesPopupView;
    [SerializeField] private LivesController _livesController;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    private Lives _lives;

    public void Awake()
    {
        InitializeData();

        InitializeLives();
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

        _livesView.Initialize(_lives);

        _livesPopupView.Initialize(_lives);
  
        _livesController.Initialize(_lives);

        _lives.StartGame();
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.GameData = new GameData();
    }
}
