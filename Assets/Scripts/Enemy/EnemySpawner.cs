using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemiesStartPoolCount;
    [SerializeField] private float spawnPositionOffset;
    [SerializeField] private float spawnPositionDistance = 2;
    [SerializeField] private float spawnInterval = 2;
    [SerializeField] private float difficultyTimerInterval = 10;
    [SerializeField] private int numberOfEnemiesToSpawn;

    private TimerService _spawnTimer;
    private TimerService _difficultyTimer;
    
    private IGameFactory _gameFactory;
    private ICameraProvider _cameraProvider;

    [Inject]
    public void Construct(IGameFactory gameFactory, ICameraProvider cameraProvider)
    {
        _cameraProvider = cameraProvider;
        _gameFactory = gameFactory;
    }

    private void Awake()
    {
        _spawnTimer = new TimerService(spawnInterval);
        _difficultyTimer = new TimerService(difficultyTimerInterval);
    }

    private void Start()
    {
        SpawnEnemies(0, enemiesStartPoolCount);
        SpawnEnemies(1,enemiesStartPoolCount);
    }

    private void Update()
    {
        _spawnTimer.UpdateTimer();
        _difficultyTimer.UpdateTimer();
        if (_spawnTimer.CheckTimerEnd())
        {
            SpawnEnemies(0,numberOfEnemiesToSpawn);
            SpawnEnemies(1,numberOfEnemiesToSpawn);
            _spawnTimer.ResetTimer();
        }

        if (_difficultyTimer.CheckTimerEnd())
        {
            //ChangeSpawnDifficulty();
            _difficultyTimer.ResetTimer();
        }
    }
    
    private void SpawnEnemies(int id, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var pos = GetRandomSpawnPointOffScreen();
            GameObject enemy = _gameFactory.CreateEnemy(id, pos, null);
        }
    }

    private void ChangeSpawnDifficulty()
    {
        numberOfEnemiesToSpawn += 1;
        spawnInterval -= 0.02f;
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