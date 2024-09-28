using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        try
        {
            col.GetComponentInParent<IDamageable>().Damage(1);
        }
        catch(Exception e)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}