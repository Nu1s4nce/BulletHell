using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IConfigProvider _configProvider;
    private readonly IHeroProvider _heroProvider;
    private readonly DiContainer _diContainer;
    
    private List<EnemyConfigData> _enemyFactory;
    
    public GameFactory(IConfigProvider configProvider, IHeroProvider heroProvider, DiContainer diContainer)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _diContainer = diContainer;
    }

    public GameObject CreateEnemy(int enemyId, Vector3 pos, Transform enemiesPoolParent)
    {
        EnemyConfigData config = _configProvider.GetEnemyConfig(enemyId);
        GameObject enemy = _diContainer.InstantiatePrefab(config.EnemyPrefab, pos, Quaternion.identity, enemiesPoolParent);
        return enemy;
    }
    public GameObject CreateHero(Vector3 pos)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject hero = _diContainer.InstantiatePrefab(config.HeroPrefab, pos, Quaternion.identity, null);
        _heroProvider.Hero = hero;
        return hero;
    }
    public GameObject CreateProjectile(Vector3 pos)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject projectile = _diContainer.InstantiatePrefab(config.WeaponPrefab, pos, Quaternion.identity, null);
        return projectile;
    }
    
    public GameObject CreateTextPopup(Vector3 pos)
    {
        GameObject textPrefab = _configProvider.GetTextPrefab();
        GameObject textPopup = _diContainer.InstantiatePrefab(textPrefab, pos, Quaternion.identity, null);
        return textPopup;
    }
    public GameObject CreateCollectable(Vector3 pos)
    {
        GameObject collectablePrefab = _configProvider.GetCollectablePrefab();
        GameObject collectable = _diContainer.InstantiatePrefab(collectablePrefab, pos, Quaternion.identity, null);
        return collectable;
    }
}