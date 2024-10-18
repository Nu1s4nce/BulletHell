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
    void AddProgressDamage(float dmg);
    void AddProgressMaxHealth(float hp);
    void AddProgressMoveSpeed(float ms);
    void AddProgressAttackRange(float atkRange);
    void AddProgressAttackRate(float atkRate);
    void AddProgressProjectileSpeed(float projSpeed);
    void AddProgressCollectablesPickRange(float pickRange);
    void AddProgressCollectablesValueBoost(int valueBoost);
    void AddProgresstMultiShot(int multiShot);
}