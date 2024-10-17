using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private IConfigProvider _configProvider;
    private IGameFactory _gameFactory;
    private IHpProvider _hpProvider;
    private IProgressService _progressService;

    [Inject]
    private void Construct(IConfigProvider configProvider, IGameFactory gameFactory, IHpProvider hpProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _hpProvider = hpProvider;
        _configProvider = configProvider;
        _gameFactory = gameFactory;
    }

    public void Initialize()
    {
        _configProvider.Load();
        _gameFactory.CreateHero(new Vector3(0,0,0));
        
        _hpProvider.SetHeroHp(_hpProvider.GetHeroMaxHp());
        _progressService.SetMainCurrency(1000);
    }
}
