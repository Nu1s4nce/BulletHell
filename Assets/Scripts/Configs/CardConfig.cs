using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsConfig")]
[Serializable]
public class CardsConfig : SerializedScriptableObject
{
    public Dictionary<TypeOfCard, float> TypeOfCard;
    public Dictionary<RarenessOfCard, float> RarenessOfCards;
    public List<TypeOfWeapon> TypeOfWeapon;

    // public float DamageBoost;
    // public float HealthBoost;
    // public float AttackRangeBoost;
    // public float AttackRateBoost;
    // public float ProjectileSpeedBoost;
    // public int MultiShotBoost;
    //
    // private int _boostsCount;
}

public enum TypeOfCard
{
    Normal = 0,
    Unique = 1
}
public enum RarenessOfCard
{
    Common,
    Rare,
    Mythic,
    Legendary,
    Rainbow
}

public enum TypeOfWeapon
{
    Hero = 0,
    Axe = 1,
    ToxicRing = 2
}
