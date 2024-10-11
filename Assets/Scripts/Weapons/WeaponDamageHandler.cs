using UnityEngine;

public class WeaponDamageHandler : MonoBehaviour
{
    public void DealDamage(Transform target, int damage)
    {
        target.GetComponent<IDamageable>().ApplyDamage(damage);
    }
}