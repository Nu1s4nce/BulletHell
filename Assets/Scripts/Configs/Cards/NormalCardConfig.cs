using UnityEngine;

public class NormalCardConfig
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
    public float DamageBoost;
    public float HealthBoost;
    public float AttackRangeBoost;
    public float AttackRateBoost;
    public float ProjectileSpeedBoost;
    public int MultiShotBoost;
}