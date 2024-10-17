using System;

public class ProgressService : IProgressService
{
    public PlayerProgressData ProgressData { get; set; } = new();
    
    public event Action CurrencyAmountChanged;

    public int GetMainCurrency()
    {
        return ProgressData.MainCurrency;
    }
    public void SetMainCurrency(int count)
    {
        ProgressData.MainCurrency = count;
        CurrencyAmountChanged?.Invoke();
    }
    public void AddMainCurrency(int count)
    {
        ProgressData.MainCurrency += count;
        CurrencyAmountChanged?.Invoke();
    }
    public void RemoveMainCurrency(int count)
    {
        ProgressData.MainCurrency -= count;
        CurrencyAmountChanged?.Invoke();
    }

    public HeroProgressData GetHeroData()
    {
        return ProgressData.HeroData;
    }
}