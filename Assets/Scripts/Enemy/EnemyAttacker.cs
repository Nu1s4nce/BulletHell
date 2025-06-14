﻿using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttacker : MonoBehaviour, IIdHolder
{
    [SerializeField] private AttackCollisionHandler _attackCollisionHandler;
    [SerializeField] private Collider2D _collider2D;
    private int _enemyId;

    private EnemyAnimator _enemyAnimator;

    private Timer _timer;

    public EnemyAttackType attackType;

    private IConfigProvider _configProvider;
    private IHeroProvider _heroProvider;
    private IGameFactory _gameFactory;
    private ITimeService _time;
    private IProgressService _progressService;

    [Inject]
    public void Construct(IConfigProvider configProvider, IHeroProvider heroProvider, IGameFactory gameFactory,
        ITimeService timeService, IProgressService progressService)
    {
        _progressService = progressService;
        _time = timeService;
        _gameFactory = gameFactory;
        _heroProvider = heroProvider;
        _configProvider = configProvider;
    }

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    
    
    private void Start()
    {
        _timer = new Timer(GetEnemyConfig().AttackRate - _progressService.GetEnemyProgressData().EnemyStatsData[_enemyId][EnemyStats.AttackRate], _time);
        if (attackType == EnemyAttackType.Melee)
            _attackCollisionHandler.onAttackZoneEnter += DealDamageToHero;
    }

    private void Update()
    {
        _timer.UpdateTimer();
        if (CheckIfCanAttack() && _timer.CheckTimerEnd())
        {
            Attack();
            _timer.ResetTimer();
        }
    }

    private bool CheckIfCanAttack()
    {
        return Vector3.Distance(transform.position, _heroProvider.GetHeroPosition()) <=
               GetEnemyConfig().DistanceToAttack;
    }

    private void Attack()
    {
        _enemyAnimator.PlayAttack();
    }

    private void DealDamageToHero()
    {
        if (!_heroProvider.Hero) return;
        _heroProvider.Hero.GetComponentInChildren<IDamageable>().ApplyDamage(GetEnemyConfig().Damage);
    }

    public void CreateEnemyProjectile()
    {
        _gameFactory.CreateCollisionProjectile(
            GetEnemyConfig().EnemyProjectilePrefab,
            transform.position,
            _heroProvider.Hero.transform,
            GetEnemyConfig().Damage + _progressService.GetEnemyProgressData().EnemyStatsData[_enemyId][EnemyStats.Damage],
            GetEnemyConfig().ProjectileSpeed + _progressService.GetEnemyProgressData().EnemyStatsData[_enemyId][EnemyStats.ProjectileSpeed],
            _collider2D
        );
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

public enum EnemyAttackType
{
    Melee,
    Range
}