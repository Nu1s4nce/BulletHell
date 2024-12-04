using System.Collections.Generic;
using UnityEngine;

public interface IConfigProvider
{
    LevelConfigData LevelConfig { get; }
    CardsChancesConfig CardsChancesConfig { get; }
    public LootChancesConfig LootChancesConfig { get; }
    public void Load();
    CardsChancesConfig GetCardsChancesConfig();
    LootChancesConfig GetLootChancesConfig();
    CurrenciesConfigData GetCurrenciesConfig();
    CardsConfig GetCardsConfig();
    HeroConfigData GetHeroConfig();
    EnemyConfigData GetEnemyConfig(int id);
    public List<EnemyConfigData> GetEnemies();
    WeaponsConfigData GetWeaponsConfig(int id);
    GameObject GetTextPrefab();
}