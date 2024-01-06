using Newtonsoft.Json;
using System;

public class GameData
{
    private int _lives;
    private DateTime _timeToGainNextLife;
    private DateTime _lastGainLifeTime;

    public GameData()
    {
        _lives = 3;
        _timeToGainNextLife = DateTime.Now;
        _lastGainLifeTime = DateTime.Now;
    }

    [JsonConstructor]
    public GameData(int lives, DateTime timeToGainNextLife, DateTime lastGainLifeTime)
    {
        Lives = lives;
        TimeToGainNextLife = timeToGainNextLife;
        LastGainLifeTime = lastGainLifeTime;
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
}
