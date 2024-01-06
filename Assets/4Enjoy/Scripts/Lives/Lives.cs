using System;
using System.Collections;
using UnityEngine;

public class Lives
{
    public event Action<string> TimerValueChanged;
    public event Action<int> LivesChanged;

    private readonly int _livesMax = 5;
    private readonly int _livesToAdd = 1;
    private readonly int _livesGainIntervalInSeconds = 20;

    private readonly IPersistentData _persistentData;
    private readonly IDataProvider _dataProvider;

    public int MaxLives => _livesMax;
    private bool IsLivesFull => _persistentData.GameData.Lives == _livesMax;
    private bool _isRestoring = false;

    private Coroutine _restoreLivesCoroutine;

    public Lives(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
    }

    public void StartGame()
    {
        if (_restoreLivesCoroutine != null) Coroutines.StopRoutine(_restoreLivesCoroutine);
        _restoreLivesCoroutine = Coroutines.StartRoutine(RestoreLives());
    }

    public void RefillLives()
    {
        _persistentData.GameData.Lives = _livesMax;

        LivesChanged?.Invoke(_persistentData.GameData.Lives);

        StartRestoreLives();

        UpdateTimer();

        Coroutines.StopRoutine(_restoreLivesCoroutine);

        _isRestoring = false;
    }

    public int GetCurrentLives() 
        => _persistentData.GameData.Lives;

    private void AddLife()
    {
        if (IsLivesFull)
            throw new ArgumentOutOfRangeException();

        _persistentData.GameData.Lives += _livesToAdd;

        LivesChanged?.Invoke(_persistentData.GameData.Lives);

        StartRestoreLives();
    }

    private void StartRestoreLives()
    {
        if (!_isRestoring)
        {
            _persistentData.GameData.LastGainLifeTime = DateTime.Now.AddSeconds(_livesGainIntervalInSeconds);

            if (_restoreLivesCoroutine != null) Coroutines.StopRoutine(_restoreLivesCoroutine);
            _restoreLivesCoroutine = Coroutines.StartRoutine(RestoreLives());
        }
    }

    public void UseLives()
    {
        if (_persistentData.GameData.Lives < 1)
            throw new ArgumentOutOfRangeException();

        _persistentData.GameData.Lives -= _livesToAdd;

        LivesChanged?.Invoke(_persistentData.GameData.Lives);

        StartRestoreLives();
    }

    private void UpdateTimer()
    {
        if (IsLivesFull)
        {
            TimerValueChanged?.Invoke(ConstantsExtension.LIVES_FULL);
            return;
        }

        TimeSpan timeSpan = (_persistentData.GameData.TimeToGainNextLife - DateTime.Now);
        string time = timeSpan.ToString(@"mm\:ss");
        TimerValueChanged?.Invoke(time);
    }

    public IEnumerator RestoreLives()
    {
        UpdateTimer();

        _isRestoring = true;

        bool addingLivesInProgress;

        while (!IsLivesFull)
        {
            Debug.Log("Im running");
            DateTime currentDateTime = DateTime.Now;
            DateTime timeToGainNextLife = _persistentData.GameData.TimeToGainNextLife;
            addingLivesInProgress = false;

            while (currentDateTime > timeToGainNextLife)
            {
                Debug.Log("Im running 1");

                addingLivesInProgress = true;

                AddLife();

                DateTime timeToAdd = _persistentData.GameData.LastGainLifeTime > timeToGainNextLife
                    ? _persistentData.GameData.LastGainLifeTime
                    : timeToGainNextLife;

                timeToGainNextLife = timeToAdd.AddSeconds(_livesGainIntervalInSeconds);
            }

            if (addingLivesInProgress)
            {
                _persistentData.GameData.LastGainLifeTime = DateTime.Now;
                _persistentData.GameData.TimeToGainNextLife = timeToGainNextLife;
            }

            UpdateTimer();

            _dataProvider.Save();

            yield return null;
        }

        _isRestoring = false;
    }
}
