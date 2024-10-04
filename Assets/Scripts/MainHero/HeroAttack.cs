using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroAttack : MonoBehaviour
{
    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;

    private float _timer;


    [Inject]
    private void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _configProvider = configProvider;
        _targetFinder = targetFinder;
    }

    private void Awake()
    {
        _timer = GetHeroStats().AttackRate;
    }

    private void Update()
    {
        UpdateTimer();
        if (IsTimerReached())
        {
            Attack();
            RestartTimer();
        }
    }

    private void Attack()
    {
        List<Transform> nearestTargets = _targetFinder.GetXNearestTargets(GetHeroStats().MultishotTargets);
        if (nearestTargets.Count == 0) return;
        foreach (var target in nearestTargets)
        {
            if (Vector3.Distance(transform.position, target.position) <= GetHeroStats().AttackRange)
            {
                _gameFactory.CreateProjectile(
                    transform.position, 
                    target, 
                    GetHeroStats().ProjectileSpeed, 
                    GetHeroStats().Damage
                    );
            }
        }
    }

    private bool IsTimerReached()
    {
        return _timer <= 0;
    }

    private void UpdateTimer()
    {
        _timer -= Time.deltaTime;
    }

    private void RestartTimer()
    {
        _timer = GetHeroStats().AttackRate;
    }

    private HeroConfigData GetHeroStats()
    {
        return _configProvider.GetHeroConfig();
    }
}