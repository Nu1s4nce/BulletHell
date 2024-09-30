using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private IConfigProvider _configProvider;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IConfigProvider configProvider, IGameFactory gameFactory)
    {
        _configProvider = configProvider;
        _gameFactory = gameFactory;
    }

    public void Initialize()
    {
        _configProvider.Load();
        _gameFactory.CreateHero(new Vector3(0,0,0));
    }
}
