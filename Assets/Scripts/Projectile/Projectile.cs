using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int _damage;
    private void OnTriggerEnter2D(Collider2D col)
    {
        try
        {
            col.GetComponentInParent<IDamageable>().Damage(_damage);
        }
        catch(Exception e)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}