using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : ITargetFinder
{
    private readonly IHeroProvider _heroProvider;
    private List<Transform> _targetedEnemies = new();
    
    public TargetFinder(IHeroProvider heroProvider)
    {
        _heroProvider = heroProvider;
    }

    public List<Transform> GetXNearestTargets(int numberOfTargets)
    {
        List<Transform> tempList = new List<Transform>(_targetedEnemies);
        List<Transform> resList = new();
        if (_targetedEnemies.Count < numberOfTargets) numberOfTargets = _targetedEnemies.Count;
        for (int i = 0; i < numberOfTargets; i++)
        {
            Transform nearest = GetNearestTarget(tempList);
            resList.Add(nearest);
            tempList.Remove(nearest);
        }

        return resList;
    }
    private Transform GetNearestTarget(List<Transform> list)
    {
        Transform closestElement = default;
        float closestDistance = float.MaxValue;

        foreach (var element in list)
        {
            float distance = Vector3.Distance(_heroProvider.Hero.transform.position, element.position);
            
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