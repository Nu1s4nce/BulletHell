using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMover : MonoBehaviour, IIdHolder
{
    private int _enemyId;
    
    private EnemyAnimator _enemyAnimator;
    
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;
    private ITimeService _time;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider, ITimeService timeService)
    {
        _time = timeService;
        _configProvider = configProvider;
        _heroProvider = heroProvider;
    }

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        Move();
        _enemyAnimator.LookAt(_heroProvider.GetHeroPosition());
    }

    private void Move()
    {
        Vector3 moveDirection = (_heroProvider.GetHeroPosition() - transform.position).normalized;
        
        if (Vector3.Distance(_heroProvider.GetHeroPosition(), transform.position) > GetEnemyConfig().DistanceToAttack)
        {
            transform.position += moveDirection * (GetEnemyConfig().Speed * _time.DeltaTime);
        }
    }
    
    private EnemyConfigData GetEnemyConfig()
    {
        return _configProvider.GetEnemyConfig(_enemyId);
    }

    public void SetId(int id)
    {
        _enemyId = id;
    }
}