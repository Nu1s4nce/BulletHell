using UnityEngine;

public interface IConfigProvider
{
    LevelConfigData LevelConfig { get; }
    public void Load();
    HeroConfigData GetHeroConfig();
    EnemyConfigData GetEnemyConfig(int id);
    WeaponsConfigData GetWeaponsConfig(int id);
    GameObject GetTextPrefab();
    GameObject GetCollectablePrefab();
}