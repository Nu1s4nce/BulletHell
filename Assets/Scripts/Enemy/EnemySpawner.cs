using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesStartPoolCount;
    [SerializeField] private Transform _enemiesPoolParent;

    private List<Vector2> _pointsPool = new();
    
    private IEnemyPoolProvider _enemyPoolProvider;
    
    [Inject]
    public void Construct(IEnemyPoolProvider enemyPoolProvider)
    {
        _enemyPoolProvider = enemyPoolProvider;
    }

    private void Awake()
    {
        _enemyPoolProvider.Init();
        for (int i = 0; i <= _enemiesStartPoolCount; i++)
        {
            var randPoint = new Vector2(Random.Range(-0.7f, 0f), Random.Range(0f, -0.7f));
            _pointsPool.Add(randPoint);
        }
    }

    private void Start()
    {
        for (int i = 0; i <= _enemiesStartPoolCount; i++)
        {
            GameObject enemy = _enemyPoolProvider.GetEnemy();
            enemy.transform.position = _pointsPool[i];
        }
    }
    

    
}