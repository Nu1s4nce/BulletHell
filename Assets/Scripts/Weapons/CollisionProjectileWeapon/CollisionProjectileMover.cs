using UnityEngine;
using Zenject;

public class CollisionProjectileMover : MonoBehaviour
{
    public float ProjectileDamage;
    public float ProjectileSpeed;
    public Transform Target;
    public Collider2D OwnCollider;
    private Rigidbody2D rb;

    private Vector2 _direction;
    private bool _pausedBefore;
    
    private ITargetFinder _targetFinder;
    private ITimeService _time;
    
    private Timer _destroyTimer;

    [Inject]
    public void Construct(ITimeService timeService)
    {
        _time = timeService;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _destroyTimer = new Timer(2f, _time);
    }
    

    private void Start()
    {
        _direction = (Target.position - transform.position).normalized;
        RotateProjectile();
        Move();
    }

    private void Update()
    {
        _destroyTimer.UpdateTimer();
        if (_destroyTimer.CheckTimerEnd())
        {
            Invoke(nameof(DestroySelf), 2);
            _destroyTimer.ResetTimer();
        }
        
        if (_time.IsPaused)
        {
            rb.linearVelocity = Vector2.zero;
            _pausedBefore = true;
        }

        if (_pausedBefore && !_time.IsPaused)
        {
            CancelInvoke(nameof(DestroySelf));
            Invoke(nameof(DestroySelf), 2);
            rb.linearVelocity = _direction * (ProjectileSpeed * _time.DeltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent(out IDamageable damageable)) return;
        if(col != OwnCollider) damageable.ApplyDamage(ProjectileDamage);
    }
    private void Move()
    {
        rb.AddForce(_direction * ProjectileSpeed * _time.DeltaTime, ForceMode2D.Impulse);
    }

    private void RotateProjectile()
    {
        transform.right = _direction;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
