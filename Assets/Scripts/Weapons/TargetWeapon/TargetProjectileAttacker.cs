using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TargetProjectileAttacker : MonoBehaviour
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
        _timer = new Timer(
            GetWeaponStats().AttackRate - _progressService.GetHeroData().HeroStatsData[StatId.AttackRate], _time);
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
        _timer.ChangeTimerMaxTime(GetWeaponStats().AttackRate -
                                  _progressService.GetHeroData().HeroStatsData[StatId.AttackRate]);
    }

    private void Attack()
    {
        List<Transform> nearestTargets = _targetFinder.GetXNearestTargets(GetWeaponStats().MultishotTargets +
                                                                          (int) _progressService.GetHeroData()
                                                                              .HeroStatsData[StatId.MultiShotTargets]);
        if (nearestTargets.Count == 0) return;
        foreach (var target in nearestTargets)
        {
            if (Vector3.Distance(transform.position, target.position) <= GetWeaponStats().AttackRange +
                _progressService.GetHeroData().HeroStatsData[StatId.AttackRange])
            {
                _gameFactory.CreateTargetProjectile(
                    GetWeaponStats().weaponProjectilePrefab,
                    transform.position,
                    target,
                    GetWeaponStats().Damage,
                    GetWeaponStats().ProjectileSpeed +
                    _progressService.GetHeroData().HeroStatsData[StatId.ProjectileSpeed]
                );
            }
        }
    }

    private WeaponsConfigData GetWeaponStats()
    {
        return _configProvider.GetWeaponsConfig(_weaponId);
    }
}