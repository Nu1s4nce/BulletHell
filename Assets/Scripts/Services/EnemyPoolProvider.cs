using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolProvider : IEnemyPoolProvider
{
    private IGameFactory _gameFactory;
    private ITargetFinder _targetFinder;

    private ObjectPool<GameObject> _enemiesPool;
    private IConfigProvider _configProvider;

    public EnemyPoolProvider(IGameFactory gameFactory, ITargetFinder targetFinder, IConfigProvider configProvider)
    {
        _configProvider = configProvider;
        _targetFinder = targetFinder;
        _gameFactory = gameFactory;
    }
    
    public void Init()
    {
        _enemiesPool = new ObjectPool<GameObject>(
            CreateEnemy,
            OnGetEnemy,
            OnReleaseEnemy,
            OnDestroyEnemy,
            maxSize: 300
        );
    }

    private void OnDestroyEnemy(GameObject enemy)
    {
        //Destroy(enemy);
    }

    private void OnReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private void OnGetEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
    }

    private GameObject CreateEnemy()
    {
        var enemy = _gameFactory.CreateEnemy(0, Vector3.zero, null);
        _targetFinder.AddTarget(enemy.transform);
        //SetupEnemyStats(enemy, 0);
        return enemy;
    }
    // private void SetupEnemyStats(GameObject enemy, int enemyId)
    // {
    //     enemy.GetComponent<EnemyStats>().SetupEnemyStats(
    //         _configProvider.GetEnemyConfig(enemyId).MaxHp,
    //         _configProvider.GetEnemyConfig(enemyId).Damage,
    //         _configProvider.GetEnemyConfig(enemyId).Speed,
    //         _configProvider.GetEnemyConfig(enemyId).AttackType
    //     );
    // }
    
    public GameObject GetEnemy()
    {
        GameObject enemyTemp = _enemiesPool.Get();
        _targetFinder.AddTarget(enemyTemp.transform);
        //SetupEnemyStats(enemyTemp, 0);
        return enemyTemp;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        _targetFinder.RemoveTarget(enemy.transform);
        _enemiesPool.Release(enemy);
    }
}