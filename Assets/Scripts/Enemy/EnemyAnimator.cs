using DG.Tweening;
using UnityEngine;
using Zenject;

public class EnemyAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [SerializeField] private Color _onDamageColor;
    private Tween _onDamageTween;

    private readonly int _attacking = Animator.StringToHash("Attacking");
    
    private ITargetFinder _targetFinder;

    [Inject]
    public void Construct(ITargetFinder targetFinder)
    {
        _targetFinder = targetFinder;
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_animator.SetFloat("AttackSpeedMultiplier", 2.0f);
    }

    public void PlayDamageReceive()
    {
        if (!gameObject) return;
        _onDamageTween.Kill();
        _spriteRenderer.color = _onDamageColor;
        _onDamageTween = _spriteRenderer.DOColor(Color.white, 0.2f);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(_attacking);
    }

    public void LookAt(Vector3 target)
    {
        transform.rotation =
            target.x > transform.position.x ?
                new Quaternion(0, 0, 0, 0) :
                new Quaternion(0, -180, 0, 0);
    }

    private void OnDestroy()
    {
        _onDamageTween.Kill();
    }

    public void PlayDeadAndDestroyObject()
    {
        _targetFinder.RemoveTarget(transform);
        Destroy(gameObject);
    }
}