using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private LevelConfigData levelConfigData;

    private EnemyFactory _factory;

    private void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(levelConfigData);

       // _factory.CreateMob("MeleeMob", 1);
    }
}