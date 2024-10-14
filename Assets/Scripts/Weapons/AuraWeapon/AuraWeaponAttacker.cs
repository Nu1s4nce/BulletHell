using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponDamageHandler))]
public class AuraWeaponAttacker : MonoBehaviour
{
    private TimerService _timer;
    private List<Transform> _targetsList;
    
    private WeaponDamageHandler _weaponDamageHandler;
    
    private ITargetFinder _targetFinder;
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(IHeroProvider heroProvider, ITargetFinder targetFinder, IConfigProvider configProvider)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _targetFinder = targetFinder;
    }
    private void Awake()
    {
        _weaponDamageHandler = GetComponent<WeaponDamageHandler>();
        
        _timer = new TimerService(GetWeaponStats().AttackRate);
        _targetFinder.targetsChanged += UpdateTargetsList;
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
        if(_targetsList.Count <= 0) return;
        foreach (var target in _targetsList.ToList())
        {
            if (Vector3.Distance(_heroProvider.GetHeroPosition(), target.position) <= GetWeaponStats().AttackRange)
            {
                if(target.gameObject.activeSelf)
                    _weaponDamageHandler.DealDamage(target, GetWeaponStats().Damage);
            }
        }
    }

    private WeaponsConfigData GetWeaponStats()
    {
        return _configProvider.GetWeaponsConfig(1);
    }

    private void UpdateTargetsList()
    {
        _targetsList = _targetFinder.GetEnemiesList();
    }

    private void OnDestroy()
    {
        _targetFinder.targetsChanged -= UpdateTargetsList;
    }
}
