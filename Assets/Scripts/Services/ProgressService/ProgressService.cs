using System;

public class ProgressService : IProgressService
{
    private IHpProvider _hpProvider;
    public PlayerProgressData ProgressData { get; set; } = new();
    
    public event Action CurrencyAmountChanged;

    public ProgressService(IHpProvider hpProvider)
    {
        _hpProvider = hpProvider;
    }

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

    public void AddProgressDamage(float dmg)
    {
        ProgressData.HeroData.DamageBonus += dmg;
    }
    public void AddProgressMaxHealth(float hp)
    {
        ProgressData.HeroData.MaxHealthBonus += hp;
        _hpProvider.AddHeroMaxHp(hp);
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