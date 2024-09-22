using UnityEngine;

public class ConfigProvider : MonoBehaviour
{
    private const string LevelConfigPath = "Configs/LevelConfig";

    public LevelConfigData LevelConfig { get; private set; }

    // Update is called once per frame
    public void Load()
    {
        LevelConfig = Resources.Load<LevelConfigData>(LevelConfigPath);
    }
}
