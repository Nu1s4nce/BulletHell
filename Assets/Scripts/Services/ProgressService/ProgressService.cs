using System;
using System.Collections.Generic;

public class ProgressService : IProgressService
{
    public PlayerProgressData ProgressData { get; set; } = new();
    
    public event Action CurrencyAmountChanged;
    public event Action AttackRateChanged;

    public int GetMainCurrency()
    {
        return ProgressData.CurrenciesData[0];
    }
    public void SetMainCurrency(int count)
    {
        ProgressData.CurrenciesData[Currencies.MainCurrency] = count;
        CurrencyAmountChanged?.Invoke();
    }
    
    public void AddMainCurrency(int count)
    {
        ProgressData.CurrenciesData[Currencies.MainCurrency] += count;
        CurrencyAmountChanged?.Invoke();
    }
    
    public void RemoveMainCurrency(int count)
    {
        ProgressData.CurrenciesData[Currencies.MainCurrency] -= count;
        CurrencyAmountChanged?.Invoke();
    }
    
    public void AddStat(StatId statId, float value)
    {
        ProgressData.HeroData.HeroStatsData[statId] += value;
    }

    public void InitStats()
    {
        foreach (StatId stat in (StatId[]) Enum.GetValues(typeof(StatId)))
        {
            ProgressData.HeroData.HeroStatsData.Add(stat, 0);
        }
    }
    public void InitPurchasedCardCount(List<int> allIds)
    {
        foreach (int id in allIds)
        {
            ProgressData.PurchasedCardCount.Add(id, 0);
        }
    }

    public HeroProgressData GetHeroData()
    {
        return ProgressData.HeroData;
    }
}