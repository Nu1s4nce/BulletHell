using UnityEngine;
using Zenject;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _enemyId;
    
    private TimerService _timer;
    
    private IConfigProvider _configProvider;
    private IHeroProvider _heroProvider;

    [Inject]
    public void Construct(IConfigProvider configProvider, IHeroProvider heroProvider)
    {
        _heroProvider = heroProvider;
        _configProvider = configProvider;
    }
    private void Awake()
    {
        _timer = new TimerService(GetEnemyStats().AttackRate);
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
        return Vector3.Distance(transform.position, _heroProvider.GetHeroPosition()) <= GetEnemyStats().DistanceToAttack;
    }

    private void Attack()
    {
        if (_heroProvider.Hero.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(GetEnemyStats().Damage);
            //вызов анимации атаки
        }
            
    }

    private EnemyConfigData GetEnemyStats()
    {
        return _configProvider.GetEnemyConfig(_enemyId);
    }
}