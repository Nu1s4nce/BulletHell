using System;
using System.Collections.Generic;

[Serializable]
public class EnemyProgressData
{
    public Dictionary<int, Dictionary<EnemyStats, float>> EnemyStatsData = new();
}

public enum EnemyStats
{
    MaxHp,
    Damage,
    MoveSpeed,
    AttackRate,
    ProjectileSpeed
}