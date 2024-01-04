using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LivesView _livesView;

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
        _lives = new Lives(_persistentData);

        _livesView.Initialize(_lives);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentData.GameData = new GameData();
    }
}
