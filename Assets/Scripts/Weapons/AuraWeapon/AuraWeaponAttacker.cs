using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponDamageHandler))]
public class AuraWeaponAttacker : MonoBehaviour
{
    [SerializeField] private int _weaponId;
    
    private Timer _timer;
    private List<Transform> _targetsList;
    
    private WeaponDamageHandler _weaponDamageHandler;
    
    private ITargetFinder _targetFinder;
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;
    private IProgressService _progressService;
    private ITimeService _time;

    [Inject]
    public void Construct(IHeroProvider heroProvider, ITargetFinder targetFinder, IConfigProvider configProvider, IProgressService progressService, ITimeService timeService)
    {
        _time = timeService;
        _progressService = progressService;
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _targetFinder = targetFinder;
    }
    private void Awake()
    {
        _progressService.AttackRateChanged += UpdateAttackRate;
        _targetFinder.targetsChanged += UpdateTargetsList;
        
        _weaponDamageHandler = GetComponent<WeaponDamageHandler>();
        
        _targetsList = new List<Transform>();
        
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
        if(_targetsList.Count <= 0) return;
        foreach (var target in _targetsList.ToList())
        {
            if (Vector3.Distance(_heroProvider.GetHeroPosition(), target.position) 
                <= GetWeaponStats().AttackRange + _progressService.GetHeroData().HeroStatsData[StatId.AttackRange])
            {
                if(target.gameObject.activeSelf)
                    _weaponDamageHandler.DealDamage(target, GetWeaponStats().Damage);
            }
        }
    }

    private WeaponsConfigData GetWeaponStats()
    {
        return _configProvider.GetWeaponsConfig(_weaponId);
    }

    private void UpdateTargetsList()
    {
        _targetsList = _targetFinder.GetEnemiesList();
    }

    private void OnDestroy()
    {
        _targetFinder.targetsChanged -= UpdateTargetsList;
        _progressService.AttackRateChanged -= UpdateAttackRate;
    }
}
