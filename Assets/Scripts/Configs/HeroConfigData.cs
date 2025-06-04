using System;
using UnityEngine;


[Serializable]
public class HeroConfigData
{
    public float MaxHealth;
    public float MoveSpeed;
    public float FoodHealValue;
    public float CollectablesPickRange;
    public float CollectablesValue;
    public float CollectablesNumberMin;
    public float CollectablesNumberMax;
    
    public GameObject WeaponPrefab;
    public GameObject HeroPrefab;
}

[Serializable]
public enum Weapon
{
    Axe = 0,
    Spear = 1
}