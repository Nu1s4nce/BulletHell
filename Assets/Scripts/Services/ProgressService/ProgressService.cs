using System;

public class ProgressService : IProgressService
{
    public PlayerProgressData ProgressData { get; set; } = new();
    
    public event Action CurrencyAmountChanged;

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
    public void AddProgressDamage(float dmg)
    {
        ProgressData.HeroData.DamageBonus += dmg;
    }
    public void AddProgressMoveSpeed(float ms)
    {
        ProgressData.HeroData.MoveSpeedBonus += ms;
    }
    public void AddProgressAttackRange(float atkRange)
    {
        ProgressData.HeroData.AttackRangeBonus += atkRange;
    }
    public void AddProgressAttackRate(float atkRate)
    {
        ProgressData.HeroData.AttackRateBonus += atkRate;
    }
    public void AddProgressProjectileSpeed(float projSpeed)
    {
        ProgressData.HeroData.ProjectileSpeedBonus += projSpeed;
    }
    public void AddProgressCollectablesPickRange(float pickRange)
    {
        ProgressData.HeroData.CollectablesPickRangeBonus += pickRange;
    }
    public void AddProgressCollectablesValueBoost(int valueBoost)
    {
        ProgressData.HeroData.CollectablesValueBonus += valueBoost;
    }
    public void AddProgresstMultiShot(int multiShot)
    {
        ProgressData.HeroData.MultishotTargetsBonus += multiShot;
    }

    public HeroProgressData GetHeroData()
    {
        return ProgressData.HeroData;
    }
}