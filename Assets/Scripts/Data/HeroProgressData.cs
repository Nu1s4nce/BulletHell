using System;
using System.Collections.Generic;

[Serializable]
public class HeroProgressData
{
    public Dictionary<StatId, float> HeroStatsData = new();
}

public enum StatId
{
    Damage,
    MaxHealth,
    MoveSpeed,
    AttackRange,
    AttackRate,
    ProjectileSpeed,
    CollectablesPickRange,
    CollectablesValue,
    MultiShotTargets,
    FoodHealValue
}