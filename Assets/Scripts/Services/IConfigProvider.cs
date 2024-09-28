using UnityEngine;

public interface IConfigProvider
{
    LevelConfigData LevelConfig { get; }
    public void Load();
    EnemyConfigData GetEnemyConfig(int id);
    HeroConfigData GetHeroConfig();
    GameObject GetTextPrefab();
}