using DG.Tweening;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Tween _onDamageTween;
    
    [SerializeField] private Color _onDamageColor;
    [SerializeField] private ParticleSystem _onDamageParticles;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }
    public void PlayRun()
    {
        _animator.Play("Run");
    }

    public void PlayDamageReceive()
    {
        if (!gameObject) return;
        _onDamageTween.Kill();
        _spriteRenderer.color = _onDamageColor;
        _onDamageTween = _spriteRenderer.DOColor(Color.white, 0.2f);
        
        //заспавнить партиклы
    }

    public void LookAt(Vector3 direction)
    {
        _spriteRenderer.flipX = direction.x < 0;
    }
}