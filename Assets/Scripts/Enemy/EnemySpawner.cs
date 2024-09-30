using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemiesStartPoolCount;
    [SerializeField] private float spawnPositionOffset;
    [SerializeField] private float spawnPositionDistance = 2;
    [SerializeField] private float spawnEveryXSeconds = 2;
    [SerializeField] private int numberOfEnemiesToSpawn;

    private float _timer;
    
    private IEnemyPoolProvider _enemyPoolProvider;
    private ICameraProvider _cameraProvider;

    [Inject]
    public void Construct(IEnemyPoolProvider enemyPoolProvider, ICameraProvider cameraProvider)
    {
        _cameraProvider = cameraProvider;
        _enemyPoolProvider = enemyPoolProvider;
    }

    private void Awake()
    {
        _timer = spawnEveryXSeconds;
        _enemyPoolProvider.Init();
    }

    private void Start()
    {
        SpawnEnemies(enemiesStartPoolCount);
    }

    private void Update()
    {
        SpawnEnemiesWithTimer(numberOfEnemiesToSpawn);
    }

    private void SpawnEnemiesWithTimer(int count)
    {
        _timer -= Time.deltaTime;
        if (!(_timer <= 0)) return;
        _timer = spawnEveryXSeconds;
        SpawnEnemies(count);
    }

    private void SpawnEnemies(int count)
    {
        List<Vector3> res = CreatePointsPool(count);

        foreach (var pos in res)
        {
            GameObject enemy = _enemyPoolProvider.GetEnemy();
            enemy.transform.position = pos;
        }
    }

    private List<Vector3> CreatePointsPool(int count)
    {
        List<Vector3> res = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            res.Add(GetRandomSpawnPointOffScreen());
        }

        return res;
    }
    
    

    private Vector3 GetRandomSpawnPointOffScreen()
    {
        Vector3 randPoint = new();
        Vector3 cameraPos = _cameraProvider.Camera.transform.position;
        
        float spawnPosX = cameraPos.x + spawnPositionOffset; // 2 по x \\ должно быть от 2 до 4 и от -2 до -4
        float spawnPosY = cameraPos.y + spawnPositionOffset; // 2 по y \\ должно быть от 2 до 4 и от -2 до -4
        
        float randFinalX = Random.value < 0.5f ?
            Random.Range(spawnPosX, spawnPosX + spawnPositionDistance) :
            Random.Range(-spawnPosX, -spawnPosX - spawnPositionDistance);
        float randFinalY = Random.value < 0.5f ?
            Random.Range(spawnPosY, spawnPosY + spawnPositionDistance) :
            Random.Range(-spawnPosY, -spawnPosY - spawnPositionDistance);

        randPoint = new Vector3(randFinalX, randFinalY, 0);

        return randPoint;
    }
    
}