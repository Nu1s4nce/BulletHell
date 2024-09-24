using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private IConfigProvider _configProvider;
    private IHeroProvider _heroProvider;
    private ITargetFinder _targetFinder;
    
    [Inject]
    private void Construct(IConfigProvider configProvider, IHeroProvider heroProvider, ITargetFinder targetFinder)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _targetFinder = targetFinder;
    }

    public void Initialize()
    {
        _configProvider.Load();
        //_heroProvider.HeroPosition
    }
}
