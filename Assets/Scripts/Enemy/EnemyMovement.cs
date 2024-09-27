using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    private EnemyConfigData _enemyData;
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;

    private SpriteRenderer _sprite;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider)
    {
        _heroProvider = heroProvider;
        _configProvider = configProvider;
    }

    private void Awake()
    {
        _enemyData = _configProvider.GetEnemyConfig(0);
    }

    private void Update()
    {
        Vector3 distanceFromPlayer = (_heroProvider.GetHeroPosition() - transform.position).normalized;
        if (Vector3.Distance(_heroProvider.GetHeroPosition(), transform.position) > 1.0f)
        {
            transform.position += (distanceFromPlayer * _enemyData.Speed * Time.deltaTime);
        }
    }
}