using UnityEngine;
using Zenject;

public class HeroDamageHandler : MonoBehaviour, IDamageable
{
    private float _currentHp;
    
    private IHpProvider _hpProvider;
    [SerializeField] private HeroAnimator _heroAnimator;

    [Inject]
    public void Construct(IHpProvider hpProvider)
    {
        _hpProvider = hpProvider;
    }

    public void ApplyDamage(float damage)
    {
        _hpProvider.RemoveHeroCurrentHp(damage);
        _heroAnimator.PlayDamageReceive();
        
        if (_hpProvider.GetHeroCurrentHp() <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}