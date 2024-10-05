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
            var pos = GetRandomSpawnPointOffScreen();
            GameObject enemy = _enemyPoolProvider.GetEnemy();
            enemy.transform.position = pos;
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

        float spawnPosX = Random.Range(
            cameraPos.x + spawnPositionOffset + spawnPositionDistance,
            cameraPos.x - spawnPositionOffset - spawnPositionDistance
            );
        
        float spawnPosY;

        if (spawnPosX >= cameraPos.x - spawnPositionOffset && spawnPosX <= cameraPos.x + spawnPositionOffset)
        {
            spawnPosY = Random.value < 0.5f ?
                Random.Range(cameraPos.y + spawnPositionOffset, cameraPos.y + spawnPositionOffset + spawnPositionDistance) :
                Random.Range(cameraPos.y - spawnPositionOffset, cameraPos.y - spawnPositionOffset - spawnPositionDistance);
        }
        else
        {
            spawnPosY = Random.Range(cameraPos.y - spawnPositionOffset - spawnPositionDistance, cameraPos.y + spawnPositionOffset + spawnPositionDistance);
        }
        
        return new Vector3(spawnPosX, spawnPosY, 0);
    }
}