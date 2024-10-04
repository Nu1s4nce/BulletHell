using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemiesStartPoolCount;
    [SerializeField] private float spawnPositionOffset;
    [SerializeField] private float spawnPositionDistance = 2;
    [SerializeField] private float spawnInterval = 2;
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
        _timer = spawnInterval;
        _enemyPoolProvider.Init();
    }

    private void Start()
    {
        SpawnEnemies(enemiesStartPoolCount);
    }

    private void Update()
    {
        UpdateTimer();
        if (IsTimerReached())
        {
            SpawnEnemies(numberOfEnemiesToSpawn);
            RestartTimer();
        }
    }
    
    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = _enemyPoolProvider.GetEnemy();
            enemy.transform.position = GetRandomSpawnPointOffScreen();
        }
    }
    
    private bool IsTimerReached()
    {
        return _timer <= 0;
    }

    private void UpdateTimer()
    {
        _timer -= Time.deltaTime;
    }

    private void RestartTimer()
    {
        _timer = spawnInterval;
    }
    
    private Vector3 GetRandomSpawnPointOffScreen()
    {
        Vector3 cameraPos = _cameraProvider.Camera.transform.position;
        
        float spawnPosX = cameraPos.x + spawnPositionOffset;
        float spawnPosY = cameraPos.y + spawnPositionOffset;

        float randFinalX = Random.Range(-spawnPosX - spawnPositionDistance, spawnPosX + spawnPositionDistance);
        float randFinalY;

        if (randFinalX >= -spawnPosY && randFinalX <= spawnPosY)
        {
            randFinalY = Random.value < 0.5f ?
                Random.Range(spawnPosY, spawnPosY + spawnPositionDistance) :
                Random.Range(-spawnPosY, -spawnPosY - spawnPositionDistance);
        }
        else
        {
            randFinalY = Random.Range(-spawnPosY - spawnPositionDistance, spawnPosY + spawnPositionDistance);
        }
        

        return new Vector3(randFinalX, randFinalY, 0);
    }
    
}