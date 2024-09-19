using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyDescriptions _mobDescriptions;

    private EnemyFactory _factory;

    private void Start()
    {
        _factory = new EnemyFactory();
        _factory.Init(_mobDescriptions);

        _factory.CreateMobModel("MeleeMob", 1);
    }
}