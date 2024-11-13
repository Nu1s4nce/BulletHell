using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private IConfigProvider _configProvider;
    private IGameFactory _gameFactory;
    private IHpProvider _hpProvider;
    private IProgressService _progressService;

    private List<int> cardIds = new();

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

        foreach (var item in _configProvider.GetCardsConfig().AllNormalCardsByRareness)
        {
            foreach (var card in item.Value)
            {
                cardIds.Add(card.CardId);
            }
        }
        _progressService.InitPurchasedCardCount(cardIds);
        _progressService.InitStats();
        _progressService.AddStat(StatId.MaxHealth, 1000);
        
        _gameFactory.CreateHero(new Vector3(0,0,0));
        
        _hpProvider.InitHeroHp();
        _progressService.SetMainCurrency(10000);
    }
}
