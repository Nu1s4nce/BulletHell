using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrenciesConfigData", menuName = "CurrenciesConfigData")]
public class CurrenciesConfigData : SerializedScriptableObject
{
    [Header("CurrenciesConfig")]
    public Dictionary<Currencies, Sprite> CurrenciesConfig = new();
}

public enum Currencies
{
    MainCurrency = 0,
    SecondaryCurrency = 1,
    ReserveCurrency = 2
}