using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsChancesConfig")]
[Serializable]
public class CardsChancesConfig : SerializedScriptableObject
{
    public Dictionary<CardType, float> TypeOfCard;
    public Dictionary<RarenessOfCard, float> RarenessOfCards;
}


public enum CardType
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
