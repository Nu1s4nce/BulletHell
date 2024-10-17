using System;

public interface IProgressService
{
    public PlayerProgressData ProgressData { get; set; }
    public event Action CurrencyAmountChanged;

    public int GetMainCurrency();
    public void SetMainCurrency(int count);
    public void AddMainCurrency(int count);
    public void RemoveMainCurrency(int count);
    public HeroProgressData GetHeroData();
}