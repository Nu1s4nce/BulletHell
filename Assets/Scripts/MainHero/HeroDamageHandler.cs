using UnityEngine;
using Zenject;

public class HeroDamageHandler : MonoBehaviour, IDamageable
{
    private int _currentHp;
    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }
    private void Awake()
    {
        _currentHp = _configProvider.GetHeroConfig().Health;
    }

    public void ApplyDamage(int damage)
    {
        if (_currentHp <= 0)
        {
            Dead();
            return;
        }
        
        _currentHp -= damage;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}