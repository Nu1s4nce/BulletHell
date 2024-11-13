using System;
using System.Collections.Generic;

public interface IProgressService
{
    public PlayerProgressData ProgressData { get; set; }
    public event Action CurrencyAmountChanged;
    public event Action AttackRateChanged;

    public int GetMainCurrency();
    public void InitStats();
    public void SetMainCurrency(int count);
    public void AddMainCurrency(int count);
    public void RemoveMainCurrency(int count);
    public HeroProgressData GetHeroData();
    public void AddStat(StatId statId, float value);
    public void InitPurchasedCardCount(List<int> allIds);
}
