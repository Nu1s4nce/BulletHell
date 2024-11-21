using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollisionProjectileAttacker : MonoBehaviour
{
    [SerializeField] private int _weaponId;

    private TimerService _timer;

    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;
    private IProgressService _progressService;

    [Inject]
    private void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory,
        IProgressService progressService)
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
        _targetFinder = targetFinder;
        _configProvider = configProvider;
    }

    private void Awake()
    {
        _progressService.AttackRateChanged += UpdateAttackRate;
        _timer = new TimerService(GetWeaponStats().AttackRate - _progressService.GetHeroData().HeroStatsData[StatId.AttackRate]);
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
        _timer.ChangeTimerMaxTime(GetWeaponStats().AttackRate - _progressService.GetHeroData().HeroStatsData[StatId.AttackRate]);
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
                    nearestTargets[0],
                    GetWeaponStats().Damage,
                    GetWeaponStats().ProjectileSpeed + _progressService.GetHeroData().HeroStatsData[StatId.ProjectileSpeed]
                );
            }
        }
    }

    private WeaponsConfigData GetWeaponStats()
    {
        return _configProvider.GetWeaponsConfig(_weaponId);
    }
}