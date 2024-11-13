using UnityEngine;
using Zenject;

public class HeroDamageHandler : MonoBehaviour, IDamageable
{
    private float _currentHp;
    
    private IHpProvider _hpProvider;

    [Inject]
    public void Construct(IHpProvider hpProvider)
    {
        _hpProvider = hpProvider;
    }

    public void ApplyDamage(float damage)
    {
        _hpProvider.RemoveHeroCurrentHp(damage);
        
        if (_hpProvider.GetHeroCurrentHp() <= 0)
        {
            Dead();
            return;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}