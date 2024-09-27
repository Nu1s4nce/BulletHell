using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesPoolCount;
    [SerializeField] private Transform _enemiesPoolParent;
    
    private IGameFactory _factory;
    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;

    private List<GameObject> _enemiesPool = new();

    [Inject]
    public void Construct(IGameFactory gameFactory, IConfigProvider configProvider, ITargetFinder targetFinder)
    {
        _targetFinder = targetFinder;
        _factory = gameFactory;
        _configProvider = configProvider;
    }
    private void Start()
    {
        SetupEnemiesPool(0);
    }

    private void SetupEnemiesPool(int enemyId)
    {
        for (var i = 0; i < _enemiesPoolCount; i++)
        {
            var enemy = _factory.CreateEnemy(enemyId, Vector3.zero, _enemiesPoolParent);
            _targetFinder.AddTarget(enemy.transform);
            SetupEnemyStats(enemy, enemyId);
            //enemy.SetActive(false);
            _enemiesPool.Add(enemy);
        }
    }

    private void SetupEnemyStats(GameObject enemy, int enemyId)
    {
        enemy.GetComponent<EnemyStats>().SetupEnemyStats(
            _configProvider.GetEnemyConfig(enemyId).MaxHp,
            _configProvider.GetEnemyConfig(enemyId).Damage,
            _configProvider.GetEnemyConfig(enemyId).Speed,
            _configProvider.GetEnemyConfig(enemyId).AttackType
            );
    }
}