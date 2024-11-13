using System;
using UnityEngine;

public class AttackCollisionHandler : MonoBehaviour
{
    public event Action onAttackZoneEnter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        onAttackZoneEnter?.Invoke();
    }
}
