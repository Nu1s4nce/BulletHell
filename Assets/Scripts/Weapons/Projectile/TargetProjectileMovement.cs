using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WeaponDamageHandler))]
public class TargetProjectileMovement : MonoBehaviour
{
    private Vector3 _startPoint;
    private Vector3 _middlePoint;
    
    public float ProjectileSpeed { private get;set; }
    public Transform Target { private get;set; }
    public int ProjectileDamage { private get;set; }
    
    private float _startTime;
    private float _flightDistance;
    
    private WeaponDamageHandler _weaponDamageHandler;

    private void Awake()
    {
        _startTime = Time.time;
        _startPoint = transform.position;
        
        _weaponDamageHandler = GetComponent<WeaponDamageHandler>();
    }

    void Start()
    {
        _flightDistance = Vector3.Distance(transform.position, Target.position);
        _middlePoint = _startPoint + (Target.position - _startPoint) / 2 + Vector3.up * Random.Range(-0.4f, 0.4f);
        
        AngleThrowing();
    }

    private void AngleThrowing()
    {
        DOVirtual
            .Float(0f, 1f, 0.5f, UpdateFly)
            .OnComplete(() =>
            {
                _weaponDamageHandler.DealDamage(Target, ProjectileDamage);
                Destroy(gameObject);
            });
    }

    private void UpdateFly(float progress)
    {
        float distanceFlied = (Time.time - _startTime) * ProjectileSpeed;
        float flightFraction = distanceFlied / _flightDistance;

        Vector3 m1 = Vector3.Lerp(_startPoint, _middlePoint, flightFraction);
        Vector3 m2 = Vector3.Lerp(_middlePoint, Target.position, flightFraction);
        transform.position = Vector3.Lerp(m1, m2, progress);
    }
}