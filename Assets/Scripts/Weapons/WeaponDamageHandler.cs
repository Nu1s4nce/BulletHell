using UnityEngine;
using Zenject;

public class WeaponDamageHandler : MonoBehaviour
{
    private IProgressService _progressService;

    [Inject]
    public void Construct(IProgressService progressService)
    {
        _progressService = progressService;
    }
    
    public void DealDamage(Transform target, float damage)
    {
        if(target.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(damage + _progressService.GetHeroData().DamageBonus);
    }
}