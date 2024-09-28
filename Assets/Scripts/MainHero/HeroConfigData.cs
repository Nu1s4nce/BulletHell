using System;
using UnityEngine;


[Serializable]
public class HeroConfigData
{
    public int Damage;
    public int MoveSpeed;
    public int Health;
    public int DashSpeed;
    public float AttackRate;
    public float AttackRange;
    public int MultishotTargets;
    
    public GameObject WeaponPrefab;
    public GameObject HeroPrefab;
}

public enum HeroAttackType
{
    Range = 0,
    Melee = 1
}

public enum Weapon
{
    Axe = 0,
    Spear = 1
}