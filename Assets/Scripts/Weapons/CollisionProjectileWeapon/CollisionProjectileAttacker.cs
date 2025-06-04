using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollisionProjectileAttacker : MonoBehaviour
{
    [SerializeField] private int _weaponId;

    private Timer _timer;

    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;
    private IProgressService _progressService;
    private ITimeService _time;

    [Inject]
    private void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory,
        IProgressService progressService, ITimeService timeService)
    {
        _time = timeService;
        _progressService = progressService;
        _gameFactory = gameFactory;
        _targetFinder = targetFinder;
        _configProvider = configProvider;
    }

    private void Awake()
    {
        _progressService.AttackRateChanged += UpdateAttackRate;
        _timer = new Timer(GetAttackRate(), _time);
    }

    private void Update()
    {
        _timer.UpdateTimer();
        if (_timer.CheckTimerEnd())
        {
            Attack();
            _timer.ResetTimer();
        }
    }

    private void UpdateAttackRate()
    {
        _timer.ChangeTimerMaxTime(GetAttackRate());
    }
    
    private float GetAttackRate()
    {
        return (GetWeaponStats().AttackRate - _progressService.GetHeroData().HeroStatsData[StatId.AttackRate]) * 100 /
               (100 + _progressService.GetHeroData().HeroStatsData[StatId.AttackSpeed]);
    }

    private void Attack()
    {
        List<Transform> nearestTargets = _targetFinder.GetXNearestTargets(GetWeaponStats().MultishotTargets + (int)_progressService.GetHeroData().HeroStatsData[StatId.MultiShotTargets]);
        if (nearestTargets.Count == 0) return;
        foreach (var target in nearestTargets)
        {
            if (Vector3.Distance(transform.position, nearestTargets[0].position) <=
                GetWeaponStats().AttackRange + _progressService.GetHeroData().HeroStatsData[StatId.AttackRange])
            {
                _gameFactory.CreateCollisionProjectile(
                    GetWeaponStats().weaponProjectilePrefab,
                    transform.position,
                    target,
                    GetWeaponStats().Damage,
                    GetWeaponStats().ProjectileSpeed + _progressService.GetHeroData().HeroStatsData[StatId.ProjectileSpeed],
                    gameObject.GetComponent<Collider2D>()
                );
            }
        }
    }

    private WeaponsConfigData GetWeaponStats()
    {
        return _configProvider.GetWeaponsConfig(_weaponId);
    }
}