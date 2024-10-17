using System;
using UnityEngine;


[Serializable]
public class HeroConfigData
{
    public float MaxHealth;
    public float MoveSpeed;
    public float CollectablesPickRange;
    
    public GameObject WeaponPrefab;
    public GameObject HeroPrefab;
}

[Serializable]
public enum Weapon
{
    Axe = 0,
    Spear = 1
}