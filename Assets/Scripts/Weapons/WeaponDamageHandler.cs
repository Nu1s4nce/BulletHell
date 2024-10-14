using UnityEngine;

public class WeaponDamageHandler : MonoBehaviour
{
    public void DealDamage(Transform target, int damage)
    {
        if(target.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(damage);
    }
}