using System;

[Serializable]
public class EnemyConfigData
{
    public int EnemyId;
    public float Speed;
    public int MaxHp;
    public int Damage;
    public AttackType AttackType;
}

public enum AttackType
{
    Range = 0,
    Melee = 1
}