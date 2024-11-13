using System;
using UnityEngine;

[Serializable]
public class EnemyConfigData
{
    public int EnemyId;
    public float Speed;
    public float MaxHp;
    public float Damage;
    public float DistanceToAttack;
    public float AttackRate;
    public float ProjectileSpeed;

    public GameObject EnemyPrefab;
    public GameObject EnemyProjectilePrefab;
}
