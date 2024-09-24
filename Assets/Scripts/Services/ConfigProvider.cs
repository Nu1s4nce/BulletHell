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

    public EnemyConfigData GetEnemyConfig(int id)
    {
        return LevelConfig.Enemies[id];
    }
    public HeroConfigData GetHeroConfig()
    {
        return LevelConfig.HeroConfigData;
    }
}