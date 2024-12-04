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
    private IProgressService _progressService;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider, ITimeService timeService,
        IProgressService progressService)
    {
        _progressService = progressService;
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
            float speed = GetEnemyConfig().Speed +
                          _progressService.GetEnemyProgressData().EnemyStatsData[_enemyId][
                              EnemyStats.MoveSpeed];
            transform.position += moveDirection * (speed * _time.DeltaTime);
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