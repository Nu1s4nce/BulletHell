using UnityEngine;

public class ConfigProvider : IConfigProvider
{
    private const string LevelConfigPath = "Configs/LevelConfigData";
    public LevelConfigData LevelConfig { get; private set; }
    
    public void Load()
    {
        LevelConfig = Resources.Load<LevelConfigData>(LevelConfigPath);
        Debug.Log(LevelConfig);
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