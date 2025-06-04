using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgressData
{
    public HeroProgressData HeroData = new();
    public EnemyProgressData EnemyData = new();
    public Dictionary<Currencies, int> CurrenciesData = new();
    public Dictionary<int, int> PurchasedCardCount = new();
    
    public int rerollCostMultiplier;
    public int enemyKilled;
    public int shopDiscount;
}