using System;
using UnityEngine;


[Serializable]
public class HeroConfigData
{
    public int Damage;
    
    public int Health;
    
    public int MultishotTargets;
    
    public float AttackRate;
    public float AttackRange;
    public float MoveSpeed;
    public float DashSpeed;
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