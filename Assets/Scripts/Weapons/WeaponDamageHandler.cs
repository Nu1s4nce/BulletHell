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
        target.GetChild(0).GetComponent<IDamageable>().ApplyDamage(damage + _progressService.GetHeroData().HeroStatsData[StatId.Damage]);
    }
}