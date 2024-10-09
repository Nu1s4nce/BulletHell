using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ProjectileDamageHandler))]
public class ProjectileMovement : MonoBehaviour
{
    private Vector3 _startPoint;
    private Vector3 _middlePoint;

    public Transform _target;

    private float _projectileSpeed;
    private float _startTime;
    private float _flightDistance;
    private float _projDamage;

    private Animator _animator;
    private ProjectileDamageHandler _projectileDamageHandler;

    public void SetTarget(Transform target, float projSpeed)
    {
        _target = target;
        _projectileSpeed = projSpeed;
    }

    private void Awake()
    {
        _startTime = Time.time;
        _startPoint = transform.position;

        _animator = GetComponent<Animator>();
        _projectileDamageHandler = GetComponent<ProjectileDamageHandler>();
    }

    void Start()
    {
        _animator.Play("Flying");

        _flightDistance = Vector3.Distance(transform.position, _target.position);
        _middlePoint = _startPoint + (_target.position - _startPoint) / 2 + Vector3.up * Random.Range(-0.4f, 0.4f);

        AngleThrowing();
    }

    private void AngleThrowing()
    {
        DOVirtual
            .Float(0f, 1f, 0.5f, UpdateFly)
            .OnComplete(() =>
            {
                _projectileDamageHandler.DealDamage(_target);
                Destroy(gameObject);
            });
    }

    private void UpdateFly(float progress)
    {
        float distanceFlied = (Time.time - _startTime) * _projectileSpeed;
        float flightFraction = distanceFlied / _flightDistance;

        Vector3 m1 = Vector3.Lerp(_startPoint, _middlePoint, flightFraction);
        Vector3 m2 = Vector3.Lerp(_middlePoint, _target.position, flightFraction);
        transform.position = Vector3.Lerp(m1, m2, progress);
    }
}