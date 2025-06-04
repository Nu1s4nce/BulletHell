using System;
using System.Collections.Generic;

public interface IProgressService
{
    public PlayerProgressData ProgressData { get; set; }
    public event Action CurrencyAmountChanged;
    public event Action AttackRateChanged;
    public event Action EnemyKilled;
    public event Action<float> HPChanged;

    public int GetMainCurrency();
    public void InitStats();
    public void InitEnemyStats(int enemyId);
    public void SetMainCurrency(int count);
    public void AddMainCurrency(int count);
    public void RemoveMainCurrency(int count);
    public int GetNumberOfKills();
    public void AddNumberOfKills(int amount);
    public HeroProgressData GetHeroData();
    public EnemyProgressData GetEnemyProgressData();
    public void AddStat(StatId statId, float value);
    public void InitPurchasedCardCount(List<int> allIds);
}
