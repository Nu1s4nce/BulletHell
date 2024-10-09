using UnityEngine;

public class ProjectileDamageHandler : MonoBehaviour
{
    private int _damage;

    public void DealDamage(Transform target)
    {
        target.GetComponent<IDamageable>().ApplyDamage(_damage);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}