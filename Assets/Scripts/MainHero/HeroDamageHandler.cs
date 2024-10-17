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
        if (_hpProvider.GetHeroCurrentHp() <= 0)
        {
            Dead();
            return;
        }
        
        float curHp = _hpProvider.GetHeroCurrentHp() - damage;
        _hpProvider.SetHeroHp(curHp);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}