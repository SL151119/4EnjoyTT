using System;

public class DailyBonus
{
    public event Action<long> CoinsWasCalculated;

    private readonly IPersistentData _persistentData;
    private readonly IDataProvider _dataProvider;

    private readonly int _monthsInSeason = 3;
    private readonly int _monthOfSpringStarts = 3;
    private readonly int _lastMonthIndex = 12;

    private readonly int _coinsForFirstDay = 2;
    private readonly int _coinsForSecondDay = 3;

    private readonly double _multiplierPreviousDay = 0.6d;

    public DailyBonus(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
    }

    public bool IsNewDailyBonus()
        => DateTime.Now.Date > _persistentData.GameData.LastDailyBonusDate;

    public void SaveNewDailyBonus()
    {
        _persistentData.GameData.LastDailyBonusDate = DateTime.Now;

        _dataProvider.Save();
    }

    public void GetDailyBonusCoins()
    {
        int daysSinceCurrentSeason = CalculateDaysSinceCurrentSeason();
        
        CoinsWasCalculated?.Invoke(CalculateDailyBonusCoins(daysSinceCurrentSeason));
    }

    private int CalculateDaysSinceCurrentSeason()
    {
        DateTime currentDate = DateTime.Now;

        int calculatedMonth;
        int calculatedYear = currentDate.Year;
        int firstMonthInSeason = 0;
        int firstDay = 1;

        //0 = first month in season, 1 = second month in season, 2 = third month in season 
        int monthInSeason = currentDate.Month % _monthsInSeason;

        if (monthInSeason == firstMonthInSeason)
            return currentDate.Day; //take days, because we are already in the first month of the season

        //if current month is winter season
        if (currentDate.Month < _monthOfSpringStarts)
        {
            calculatedMonth = _lastMonthIndex;
            calculatedYear -= 1;
        }
        else
        {
            calculatedMonth = currentDate.Month - monthInSeason;
        }

        return (currentDate - new DateTime(calculatedYear, calculatedMonth, firstDay)).Days + 1; //add 1 for include the current day
    }

    private long CalculateDailyBonusCoins(int daysSinceCurrentSeason)
    {
        if (daysSinceCurrentSeason == 1)
            return _coinsForFirstDay;

        if (daysSinceCurrentSeason == 2)
            return _coinsForSecondDay;

        long coinsTwoDaysAgo = _coinsForFirstDay;
        long coinsPreviousDay = _coinsForSecondDay;

        for (int i = 3; i <= daysSinceCurrentSeason; i++)
        {
            //Int64 for convert to long
            long dailyBonusCoins = Convert.ToInt64(Math.Round(coinsTwoDaysAgo + coinsPreviousDay * _multiplierPreviousDay, 0, MidpointRounding.AwayFromZero));
            
            if (i == daysSinceCurrentSeason)
                return dailyBonusCoins;

            coinsTwoDaysAgo = coinsPreviousDay;
            coinsPreviousDay = dailyBonusCoins;
        }

        return 0;
    }
}
