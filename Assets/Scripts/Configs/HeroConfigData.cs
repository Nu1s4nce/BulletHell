using System;
using UnityEngine;


[Serializable]
public class HeroConfigData
{
    public float Damage;
    
    public float MaxHealth;
    public float CurrentHealth;
    
    public int MultishotTargets;
    
    public float AttackRate;
    public float AttackRange;
    public float MoveSpeed;
    public float ProjectileSpeed;
    public float CollectablesPickRange;
    
    
    public GameObject WeaponPrefab;
    public GameObject HeroPrefab;
}

public enum Weapon
{
    Axe = 0,
    Spear = 1
}