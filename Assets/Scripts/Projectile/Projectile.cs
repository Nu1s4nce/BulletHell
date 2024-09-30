using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int _damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponentInParent<IDamageable>() != null)
            col.GetComponentInParent<IDamageable>().Damage(_damage);
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}