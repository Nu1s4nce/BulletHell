using System;
using UnityEngine;


[Serializable]
public class HeroConfigData
{
    public int Damage;
    public int MoveSpeed;
    public int Health;
    public int DashSpeed;
    public HeroAttackType AttackType;

    public GameObject HeroPrefab;
}

public enum HeroAttackType
{
    Range = 0,
    Melee = 1
}