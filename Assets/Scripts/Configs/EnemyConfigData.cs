using System;
using UnityEngine;

[Serializable]
public class EnemyConfigData
{
    public int EnemyId;
    public float Speed;
    public int MaxHp;
    public int Damage;
    public float DistanceToAttack;

    public GameObject EnemyPrefab;
    public EnemyAttackType AttackType;
}

public enum EnemyAttackType
{
    Range = 0,
    Melee = 1
}