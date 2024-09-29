using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 _startPoint;
    private Vector3 _middlePoint;
    
    public Transform _target; 
    
    private float _projectileSpeed;
    private float _startTime;
    private float _flightDistance;

    private Animator _animator;

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    public void SetProjectileSpeed(float projSpeed)
    {
        _projectileSpeed = projSpeed;
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
        OnTargetLostDestroy();
    }

    private void AngleThrowing()
    {
        float distanceFlied = (Time.time - _startTime) * _projectileSpeed;
        float flightFraction = distanceFlied / _flightDistance;
        
        Vector3 m1 = Vector3.Lerp( _startPoint, _middlePoint, flightFraction );
        Vector3 m2 = Vector3.Lerp( _middlePoint, _target.position, flightFraction );
        transform.position = Vector3.Lerp(m1, m2, flightFraction);

    }

    private void OnTargetLostDestroy()
    {
        if(!_target.gameObject.activeSelf) Destroy(gameObject); 
    }
}