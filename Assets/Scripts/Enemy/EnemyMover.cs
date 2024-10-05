using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMover : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    private EnemyConfigData _enemyData;
    
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
    }

    private void Awake()
    {
        _enemyData = _configProvider.GetEnemyConfig(0);
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }
    private void OnEnable() => _enemyData = _configProvider.GetEnemyConfig(0);

    private void Update()
    {
        Move();
        _enemyAnimator.LookAt(_heroProvider.GetHeroPosition());
    }

    private void Move()
    {
        Vector3 distanceFromPlayer = (_heroProvider.GetHeroPosition() - transform.position).normalized;
        if (Vector3.Distance(_heroProvider.GetHeroPosition(), transform.position) > _enemyData.DistanceToAttack)
        {
            transform.position += distanceFromPlayer * (_enemyData.Speed * Time.deltaTime);
        }
        else
        {
            //Attack
        }
    }
}