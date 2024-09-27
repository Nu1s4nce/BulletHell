using UnityEngine;
using Zenject;

public class ProjectileMovement : MonoBehaviour
{
    private Transform _projectileTarget;
    private Vector3 _startPoint;
    
    private float _projectileSpeed = 0.5f;
    private float _startTime;
    private float _flightDistance;
    
    private ITargetFinder _targetFinder;

    [Inject]
    public void Construct(ITargetFinder targetFinder)
    {
        _targetFinder = targetFinder;
    }
    void Start()
    {
        // Keep a note of the time the lemon was thrown
        _startTime = Time.time;
        _startPoint = transform.position;

        // Calculate the length of the flight
        _flightDistance = Vector3.Distance(transform.position, _targetFinder.GetNearestTarget().position);
    }

    void Update()
    {
        // Distance flied = time * speed
        float distanceFlied = (Time.time - _startTime) * _projectileSpeed;

        // Fraction of flight completed
        float flightFraction = distanceFlied / _flightDistance;

        // Update the lemons position
        transform.position = Vector3.Lerp(_startPoint, _targetFinder.GetNearestTarget().position, flightFraction);
    }
}