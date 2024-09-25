using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private IConfigProvider _configProvider;
    private IHeroProvider _heroProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;
    
    [Inject]
    private void Construct(IConfigProvider configProvider, IHeroProvider heroProvider, ITargetFinder targetFinder, IGameFactory gameFactory)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _targetFinder = targetFinder;
        _gameFactory = gameFactory;
    }

    public void Initialize()
    {
        _configProvider.Load();
        _gameFactory.CreateHero(new Vector3(1,1,0));
    }
}
