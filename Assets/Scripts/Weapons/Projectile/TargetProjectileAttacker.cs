using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TargetProjectileAttacker : MonoBehaviour
{
    private TimerService _timer;
    
    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;


    [Inject]
    private void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _targetFinder = targetFinder;
        _configProvider = configProvider;
    }
    
    private void Awake()
    {
        _timer = new TimerService(GetWeaponStats().AttackRate);
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
    
    private void Attack()
    {
        List<Transform> nearestTargets = _targetFinder.GetXNearestTargets(GetWeaponStats().MultishotTargets);
        if (nearestTargets.Count == 0) return;
        foreach (var target in nearestTargets)
        {
            if (Vector3.Distance(transform.position, target.position) <= GetWeaponStats().AttackRange)
            {
                _gameFactory.CreateProjectile(
                    transform.position, 
                    target,
                    GetWeaponStats().Damage,
                    GetWeaponStats().ProjectileSpeed
                );
            }
        }
    }
    
    private WeaponsConfigData GetWeaponStats()
    {
        return _configProvider.GetWeaponsConfig(0);
    }
}