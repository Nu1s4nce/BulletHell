using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : ITargetFinder
{
    private readonly IHeroProvider _heroProvider;
    private HashSet<Transform> _targetedEnemies = new();
    
    public TargetFinder(IHeroProvider heroProvider)
    {
        _heroProvider = heroProvider;
    }
    public Transform GetNearestTarget()
    {
        Transform closestElement = default;
        float closestDistance = float.MaxValue;

        foreach (var element in _targetedEnemies)
        {
            Vector3 elementPosition = element.position;
            float distance = Vector3.Distance(_heroProvider.HeroPosition.transform.position, elementPosition);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestElement = element;
            }
        }

        return closestElement;
    }

    public void AddTarget(Transform target)
    {
        _targetedEnemies.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        _targetedEnemies.Remove(target);
    }
}