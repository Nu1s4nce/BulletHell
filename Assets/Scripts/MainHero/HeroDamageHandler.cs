using UnityEngine;
using Zenject;

public class HeroDamageHandler : MonoBehaviour, IDamageable
{
    private float _currentHp;
    
    private IHpProvider _hpProvider;
    private ISoundManager _soundService;
    [SerializeField] private HeroAnimator _heroAnimator;

    [Inject]
    public void Construct(IHpProvider hpProvider, ISoundManager soundService)
    {
        _hpProvider = hpProvider;
        _soundService = soundService;
    }

    public void ApplyDamage(float damage)
    {
        _hpProvider.RemoveHeroCurrentHp(damage);
        _heroAnimator.PlayDamageReceive();
        _soundService.PlayPlayerDamageReceive();
        
        if (_hpProvider.GetHeroCurrentHp() <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        //handle loose
    }
}