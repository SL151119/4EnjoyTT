using System;
using System.Collections;
using UnityEngine;

public class Lives
{
    public event Action<string> TimerValueChanged;
    public event Action<int> LivesChanged;

    public int MaxLives => _livesMax;
    public int CurrentLives
    {
        get => _persistentData.GameData.Lives;

        private set => _persistentData.GameData.Lives = value;
    }

    private readonly int _livesMax = 5;
    private readonly int _livesToAdd = 1;
    private readonly int _livesGainIntervalInSeconds = 20;

    private readonly IPersistentData _persistentData;
    private readonly IDataProvider _dataProvider;

    private bool _isRestoring = false;

    private Coroutine _restoreLivesCoroutine;
    private bool IsLivesFull => CurrentLives == _livesMax;

    public Lives(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
    }

    public void StartGame()
    {
        StartRestoreLivesCoroutine();
    }

    public void RefillLives()
    {
        CurrentLives = MaxLives;

        UpdateLivesCount();

        UpdateTimer();

        Coroutines.StopRoutine(_restoreLivesCoroutine);

        _isRestoring = false;
    }

    public void UseLife()
    {
        if (CurrentLives < 1)
            throw new ArgumentOutOfRangeException();

        CurrentLives -= _livesToAdd;
        UpdateLivesCount();   
    }

    private IEnumerator RestoreLives()
    {
        UpdateTimer();

        _isRestoring = true;

        bool addingLivesInProgress;

        while (!IsLivesFull)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime timeToGainNextLife = _persistentData.GameData.TimeToGainNextLife;

            addingLivesInProgress = false;

            while (currentDateTime > timeToGainNextLife)
            {
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

    private void AddLife()
    {
        if (CurrentLives < MaxLives)
        {
            CurrentLives += _livesToAdd;
            UpdateLivesCount();
        }
    }

    private void UpdateLivesCount()
    {
        LivesChanged?.Invoke(CurrentLives);

        _dataProvider.Save();

        if (!_isRestoring)
        {
            _persistentData.GameData.TimeToGainNextLife = DateTime.Now.AddSeconds(_livesGainIntervalInSeconds);

            StartRestoreLivesCoroutine();
        }
    }
    
    private void StartRestoreLivesCoroutine()
    {
        if (_restoreLivesCoroutine != null) Coroutines.StopRoutine(_restoreLivesCoroutine);
        _restoreLivesCoroutine = Coroutines.StartRoutine(RestoreLives());
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
}
