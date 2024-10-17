using System;

[Serializable]
public class HeroProgressData
{
    public float DamageBonus = 0;
    public float MaxHealthBonus = 0;
    public float CurrentHealth = 0;
    public float AttackRateBonus = 0;
    public float AttackRangeBonus = 0;
    public float MoveSpeedBonus = 0;
    public float ProjectileSpeedBonus = 0;
    public float CollectablesPickRangeBonus = 0;
    
    public int CollectablesValueBonus= 0;
    public int MultishotTargetsBonus = 0;
}