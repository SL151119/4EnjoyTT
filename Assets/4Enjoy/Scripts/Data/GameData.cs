using Newtonsoft.Json;
using System;

public class GameData
{
    private int _lives;

    public GameData()
    {
        _lives = 3;
    }

    [JsonConstructor]
    public GameData(int lives)
    {
        Lives = lives;
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
}
