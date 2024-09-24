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
}