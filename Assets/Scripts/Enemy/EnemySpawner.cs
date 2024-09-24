using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesPoolCount;
    [SerializeField] private Transform _enemiesPoolParent;
    
    private IGameFactory _factory;

    private List<GameObject> _enemiesPool = new();
    
    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
        _factory = gameFactory;
    }
    private void Start()
    {
        SetupEnemiesPool();
    }

    private void SetupEnemiesPool()
    {
        for (var i = 0; i < _enemiesPoolCount; i++)
        {
            var enemy = _factory.CreateEnemy(0, Vector3.zero, _enemiesPoolParent);
            enemy.SetActive(false);
            _enemiesPool.Add(enemy);
        }
    }
}