using System;

public class Lives
{
    public event Action<int> LivesChanged;

    private readonly IPersistentData _persistentData;

    public Lives(IPersistentData persistentData)
        => _persistentData = persistentData;

    public void RefillLives(int lives)
    {
        if (lives < 0)
            throw new ArgumentOutOfRangeException(nameof(lives));

        _persistentData.GameData.Lives += lives;

        LivesChanged?.Invoke(_persistentData.GameData.Lives);
    }

    public int GetCurrentLives() => _persistentData.GameData.Lives;

    public void UseLives(int lives)
    {
        if (lives < 0)
            throw new ArgumentOutOfRangeException(nameof(lives));

        _persistentData.GameData.Lives -= lives;

        LivesChanged?.Invoke(_persistentData.GameData.Lives);
    }
}
