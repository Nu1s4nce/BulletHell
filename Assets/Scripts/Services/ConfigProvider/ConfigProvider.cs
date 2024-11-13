using UnityEngine;

public class ConfigProvider : IConfigProvider
{
    private const string LevelConfigPath = "Configs/LevelConfigData";
    private const string CardsChancesConfigPath = "Configs/CardsChancesConfig";
    private const string CardsConfigPath = "Configs/CardsConfig";
    private const string CurrenciesConfigPath = "Configs/CurrenciesConfigData";
    
    public LevelConfigData LevelConfig { get; private set; }
    public CardsChancesConfig CardsChancesConfig { get; private set; }
    public CurrenciesConfigData CurrenciesConfigData { get; private set; }
    public CardsConfig CardsConfig { get; private set; }

    public void Load()
    {
        LevelConfig = Resources.Load<LevelConfigData>(LevelConfigPath);
        CardsChancesConfig = Resources.Load<CardsChancesConfig>(CardsChancesConfigPath);
        CurrenciesConfigData = Resources.Load<CurrenciesConfigData>(CurrenciesConfigPath);
        CardsConfig = Resources.Load<CardsConfig>(CardsConfigPath);
    }

    public CardsChancesConfig GetCardsChancesConfig()
    {
        return CardsChancesConfig;
    }
    public CurrenciesConfigData GetCurrenciesConfig()
    {
        return CurrenciesConfigData;
    }
    public CardsConfig GetCardsConfig()
    {
        return CardsConfig;
    }
    public HeroConfigData GetHeroConfig()
    {
        return LevelConfig.HeroConfigData;
    }
    public EnemyConfigData GetEnemyConfig(int id)
    {
        return LevelConfig.Enemies[id];
    }
    public WeaponsConfigData GetWeaponsConfig(int id)
    {
        return LevelConfig.Weapons[id];
    }
    public GameObject GetTextPrefab()
    {
        return LevelConfig.textPopUp;
    }
    public GameObject GetCollectablePrefab()
    {
        return LevelConfig.collectable;
    }
}