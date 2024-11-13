using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttacker : MonoBehaviour, IIdHolder
{
    [SerializeField] private AttackCollisionHandler _attackCollisionHandler;
    private int _enemyId;

    private EnemyAnimator _enemyAnimator;
    
    private TimerService _timer;
    
    public EnemyAttackType attackType;

    private IConfigProvider _configProvider;
    private IHeroProvider _heroProvider;
    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IConfigProvider configProvider, IHeroProvider heroProvider, IGameFactory gameFactory)
    {
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
        _timer = new TimerService(GetEnemyConfig().AttackRate);
        if(attackType == EnemyAttackType.Melee)
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
        Debug.Log("Атака");
        _enemyAnimator.PlayAttack();
    }
    
    private void DealDamageToHero()
    {
        if (_heroProvider.Hero.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(GetEnemyConfig().Damage);
        }
    }
    public void CreateEnemyProjectile()
    {
        _gameFactory.CreateCollisionProjectile(
            GetEnemyConfig().EnemyProjectilePrefab,
            transform.position,
            _heroProvider.Hero.transform,
            GetEnemyConfig().Damage,
            GetEnemyConfig().ProjectileSpeed
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