using UnityEngine;

public class UniqueCardConfig
{
    [Header("Card Information")]
    public int CardId;
    public Sprite CardImage;
    public Sprite CardBorder;
    public string CardName;
    public int CardsInPool;
    
    [Header("Price tag")]
    public int CardCost;
    public Sprite CardCostImage;
    
    [Header("Buffs")]
    public TypeOfWeapon WeaponType;
    
}