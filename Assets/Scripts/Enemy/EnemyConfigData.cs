using System;
using UnityEngine;

[Serializable]
public class EnemyConfigData
{
    public int EnemyId;
    public float Speed;
    public int MaxHp;
    public int Damage;

    public GameObject EnemyPrefab;
    public AttackType AttackType;
}

public enum AttackType
{
    Range = 0,
    Melee = 1
}