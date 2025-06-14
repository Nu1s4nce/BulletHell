using System;
using UnityEngine;

[Serializable]
public class WeaponsConfigData
{
    public int Id;
    public int Damage;
    public int MultishotTargets;
    public float AttackRange;
    public float AttackRate;
    public float AttackSpeed;
    public float ProjectileSpeed;
    public GameObject weaponPrefab;
    public GameObject weaponProjectilePrefab;
}
