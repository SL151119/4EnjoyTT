using Newtonsoft.Json;
using System;

public class GameData
{
    private int _lives;

    private DateTime _timeToGainNextLife;
    private DateTime _lastGainLifeTime;
    private DateTime _lastDailyBonusDate;

    public GameData()
    {
        _lives = 3;

        _timeToGainNextLife = DateTime.Now;
        _lastGainLifeTime = DateTime.Now;
        _lastDailyBonusDate = DateTime.MinValue.Date;
    }

    [JsonConstructor]
    public GameData(int lives, DateTime timeToGainNextLife, DateTime lastGainLifeTime, DateTime lastDailyBonusDate)
    {
        Lives = lives;
        TimeToGainNextLife = timeToGainNextLife;
        LastGainLifeTime = lastGainLifeTime;
        LastDailyBonusDate = lastDailyBonusDate;
    }

    public int Lives
    {
        get => _lives;

        set
        {
            if (_lives < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _lives = value;
        }
    }

    public DateTime TimeToGainNextLife
    {
        get => _timeToGainNextLife;
        set => _timeToGainNextLife = value;
    }

    public DateTime LastGainLifeTime
    {
        get => _lastGainLifeTime;
        set => _lastGainLifeTime = value;
    }

    public DateTime LastDailyBonusDate
    {
        get => _lastDailyBonusDate;
        set => _lastDailyBonusDate = value;
    }
}
