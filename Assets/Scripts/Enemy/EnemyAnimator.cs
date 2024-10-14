using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayDamageReceive()
    {
        if(gameObject.activeSelf)
            _animator.Play("OnDamage", 0, 0);
    }
    public void LookAt(Vector3 target)
    {
        _spriteRenderer.flipX = target.x < transform.position.x;
    }
}
