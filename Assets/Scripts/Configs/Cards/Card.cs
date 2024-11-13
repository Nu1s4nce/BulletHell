using UnityEngine;

public class Card
{
    [Header("Card Information")]
    public int CardId;
    public Sprite CardImage;
    public Sprite CardBorder;
    public string CardName;
    public int PoolLimit;
    
    [Header("Price tag")]
    public int CardCost;
    public Currencies CurrencyType;
}