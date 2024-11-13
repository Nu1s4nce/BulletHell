using UnityEngine;

public class EnemyProjectileMover : MonoBehaviour
{
    public float ProjectileDamage;
    public float ProjectileSpeed;
    public Transform Target;
    private Rigidbody2D rb;

    private Vector2 _direction;
    
    private ITargetFinder _targetFinder;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
        
    }
    private void Start()
    {
        _direction = (Target.position - transform.position).normalized;
        RotateProjectile();
        Move();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(ProjectileDamage);
        }
    }
    private void Move()
    {
        rb.AddForce(_direction * ProjectileSpeed ,ForceMode2D.Impulse);
    }

    private void RotateProjectile()
    {
        transform.right = _direction;
    }
}
