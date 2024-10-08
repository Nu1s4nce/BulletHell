using System.Collections.Generic;
using UnityEngine;

public interface ITargetFinder
{
    public List<Transform> GetXNearestTargets(int numberOfTargets);
    void AddTarget(Transform target);
    void RemoveTarget(Transform target);
}