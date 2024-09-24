using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private IConfigProvider _configProvider;
    private IHeroProvider _heroProvider;
    private List<EnemyConfigData> _enemyFactory;
    
    public GameFactory(IConfigProvider configProvider, IHeroProvider heroProvider)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
    }

    public GameObject CreateEnemy(int enemyId, Vector3 pos, Transform enemiesPoolParent)
    {
        Debug.Log($"хп моба: {_configProvider.GetEnemyConfig(enemyId).MaxHp}, дмг {_configProvider.GetEnemyConfig(enemyId).Damage}");
        return Object.Instantiate(_configProvider.GetEnemyConfig(enemyId).EnemyPrefab, pos, Quaternion.identity, enemiesPoolParent);
    }
    public GameObject CreateHero(Vector3 pos)
    {
        GameObject heroGO = Object.Instantiate(_configProvider.GetHeroConfig().HeroPrefab, pos, Quaternion.identity);
        _heroProvider.HeroPosition = heroGO;
        return heroGO;
    }
}