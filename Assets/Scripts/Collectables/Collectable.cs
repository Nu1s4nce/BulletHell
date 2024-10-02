using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Collectable : MonoBehaviour, ICollectable
{
    private Vector3 _startPos;
    
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
    }

    private void Awake()
    {
        _startPos = transform.position;
    }

    public void Update()
    {
        OnCollectAnimation();
    }


    public void OnCollect()
    {
        
    }

    private void OnCollectAnimation()
    {
        if (Vector3.Distance(_startPos, GetHeroTransform().position) <=
            _configProvider.GetHeroConfig().CollectablesPickRange)
        {
            FlyToHeroPosition();
        }
    }

    private void FlyToHeroPosition()
    {
        transform.DOMove(GetHeroTransform().position, 0.4f);
        if (Vector3.Distance(transform.position, GetHeroTransform().position) <= 0.05)
        {
            Destroy(gameObject);
        }
    }

    private Transform GetHeroTransform()
    {
        return _heroProvider.Hero.transform;
    }
}