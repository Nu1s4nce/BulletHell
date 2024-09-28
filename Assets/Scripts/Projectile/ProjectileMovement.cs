using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 _startPoint;
    private Vector3 _middlePoint;
    
    private Transform _target; 
    
    private float _projectileSpeed = 1f;
    private float _startTime;
    private float _flightDistance;

    private Animator _animator;
    
    private ITargetFinder _targetFinder;
    
    

    [Inject]
    public void Construct(ITargetFinder targetFinder)
    {
        _targetFinder = targetFinder;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        _startTime = Time.time;
        _startPoint = transform.position;

        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _animator.Play("Flying");
        
        _flightDistance = Vector3.Distance(transform.position, _target.position);
        _middlePoint = _startPoint + (_target.position - _startPoint) / 2 + Vector3.up * Random.Range(-0.2f, 0.2f);
    }

    void Update()
    {
        AngleThrowing();
    }

    private void AngleThrowing()
    {
        float distanceFlied = (Time.time - _startTime) * _projectileSpeed;
        float flightFraction = distanceFlied / _flightDistance;
        
        Vector3 m1 = Vector3.Lerp( _startPoint, _middlePoint, flightFraction );
        Vector3 m2 = Vector3.Lerp( _middlePoint, _target.position, flightFraction );
        transform.position = Vector3.Lerp(m1, m2, flightFraction);

    }
}