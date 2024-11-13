using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolProvider : IEnemyPoolProvider
{
    private readonly IGameFactory _gameFactory;
    private readonly ITargetFinder _targetFinder;

    private List<ObjectPool<GameObject>> _allPools;
    private ObjectPool<GameObject> _enemiesPool;
    private ObjectPool<GameObject> _hobbitsPool;

    public EnemyPoolProvider(IGameFactory gameFactory, ITargetFinder targetFinder)
    {
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
        GameObject enemy = _gameFactory.CreateEnemy(2, Vector3.zero, null);
        return enemy;
    }
    public GameObject GetEnemy()
    {
        GameObject enemyTemp = _enemiesPool.Get();
        _targetFinder.AddTarget(enemyTemp.transform);
        return enemyTemp;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        _targetFinder.RemoveTarget(enemy.transform);
        _enemiesPool.Release(enemy);
    }
}