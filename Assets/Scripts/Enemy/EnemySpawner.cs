using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesPoolCount;
    [SerializeField] private Transform _enemiesPoolParent;
    
    private IConfigProvider _configProvider;
    private EnemyFactory _factory;

    private List<GameObject> _enemiesPool = new();
    
    [Inject]
    public void Construct(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }
    private void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(_configProvider.LevelConfig.Enemies);
        
        SetupEnemiesPool();
    }

    private void SetupEnemiesPool()
    {
        for (var i = 0; i < _enemiesPoolCount; i++)
        {
            var enemy = Instantiate(_factory.CreateEnemy(0).EnemyPrefab, new Vector3(0, 0, 0), Quaternion.identity, _enemiesPoolParent);
            enemy.SetActive(false);
            _enemiesPool.Add(enemy);
        }
    }
}