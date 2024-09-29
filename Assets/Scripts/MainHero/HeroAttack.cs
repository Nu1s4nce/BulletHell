using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroAttack : MonoBehaviour
{
    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;
    
    private float timer;

    [SerializeField] private GameObject weaponPrefab;
    

    [Inject]
    private void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _configProvider = configProvider;
        _targetFinder = targetFinder;
    }

    private void Awake()
    {
        timer = GetHeroStats().AttackRate;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            List<Transform> nearestTargets = _targetFinder.GetXNearestTargets(GetHeroStats().MultishotTargets);
            if(nearestTargets.Count == 0) return;
            if(Vector3.Distance(nearestTargets[0].position, transform.position) > GetHeroStats().AttackRange) return;
            
            timer = GetHeroStats().AttackRate;
            foreach (var target in nearestTargets)
            {
                GameObject proj = _gameFactory.CreateProjectile(transform.position);
                proj.GetComponent<ProjectileMovement>().SetTarget(target);
                proj.GetComponent<ProjectileMovement>().SetProjectileSpeed(GetHeroStats().ProjectileSpeed);
                proj.GetComponent<Projectile>().SetDamage(GetHeroStats().Damage);
                
            }
        }
    }
    
    private HeroConfigData GetHeroStats()
    {
        return _configProvider.GetHeroConfig();
    }
}