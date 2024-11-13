using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WeaponDamageHandler))]
public class TargetProjectileMover : MonoBehaviour
{
    private Vector3 _startPoint;
    private Vector3 _middlePoint;

    private float _startTime;
    private float _flightDistance;

    private WeaponDamageHandler _weaponDamageHandler;

    public float ProjectileSpeed { private get; set; }
    public Transform Target { private get; set; }
    public float ProjectileDamage { private get; set; }

    private Tweener _tweener;


    private void Awake()
    {
        _startTime = Time.time;
        _startPoint = transform.position;

        _weaponDamageHandler = GetComponent<WeaponDamageHandler>();
    }

    private void Start()
    {
        _flightDistance = Vector3.Distance(transform.position, Target.position);
        _middlePoint = _startPoint + (Target.position - _startPoint) / 2 + Vector3.up * Random.Range(-0.4f, 0.4f);

        AngleThrowing();
    }

    private void Update()
    {
        
    }

    private void AngleThrowing()
    {
        _tweener = DOVirtual
            .Float(0f, 1f, 0.5f, UpdateFly)
            .OnComplete(() =>
            {
                _weaponDamageHandler.DealDamage(Target, ProjectileDamage);
                Destroy(gameObject);
            });
    }

    private void UpdateFly(float progress)
    {
        DestroyOnTargetLoss();
        if (!Target) return;
        float distanceFlied = (Time.time - _startTime) * ProjectileSpeed;
        float flightFraction = distanceFlied / _flightDistance;

        Vector3 m1 = Vector3.Lerp(_startPoint, _middlePoint, flightFraction);
        Vector3 m2 = Vector3.Lerp(_middlePoint, Target.position, flightFraction);
        transform.position = Vector3.Lerp(m1, m2, progress);
    }

    private void OnDestroy()
    {
        _tweener.Kill();
    }

    private void DestroyOnTargetLoss()
    {
        if (!Target)
        {
            _tweener.Kill();
        }
    }
}