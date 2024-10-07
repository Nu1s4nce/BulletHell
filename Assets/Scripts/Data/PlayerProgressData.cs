using System;

[Serializable]
public class PlayerProgressData
{
    public int MainCurrency;
    public int SecondaryCurrency;
    public int ReserveCurrency;
    
    public event Action CurrencyAmountChanged;
    
    public void AddMainCurrency(int count)
    {
        MainCurrency += count;
        CurrencyAmountChanged.Invoke();
    }
}