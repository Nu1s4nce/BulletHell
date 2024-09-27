using UnityEngine;

public interface ITargetFinder
{
    public Transform GetNearestTarget();
    void AddTarget(Transform target);
    void RemoveTarget(Transform target);
}