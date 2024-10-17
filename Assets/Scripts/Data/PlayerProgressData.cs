using System;

[Serializable]
public class PlayerProgressData
{
    public HeroProgressData HeroData = new();

    public int RefreshButtonCost = 2;
    public int RefreshButtonCostMultiplier = 2;
    
    public int MainCurrency;
    public int SecondaryCurrency;
    public int ReserveCurrency;
}