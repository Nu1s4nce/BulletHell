using UnityEngine;

public interface IConfigProvider
{
    LevelConfigData LevelConfig { get; }
    CardsChancesConfig CardsChancesConfig { get; }
    public void Load();
    CardsChancesConfig GetCardsChancesConfig();
    CurrenciesConfigData GetCurrenciesConfig();
    CardsConfig GetCardsConfig();
    HeroConfigData GetHeroConfig();
    EnemyConfigData GetEnemyConfig(int id);
    WeaponsConfigData GetWeaponsConfig(int id);
    GameObject GetTextPrefab();
    GameObject GetCollectablePrefab();
}