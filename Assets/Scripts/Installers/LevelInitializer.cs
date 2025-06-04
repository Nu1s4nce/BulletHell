using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInitializer : IInitializable
{
    private IConfigProvider _configProvider;
    private IGameFactory _gameFactory;
    private IHpProvider _hpProvider;
    private IProgressService _progressService;

    private List<int> _cardIds = new();

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
        Debug.Log("Initializing Level Initializer");
        _configProvider.Load();
        _progressService.ProgressData.HeroData.HeroStatsData.Clear();
        _progressService.ProgressData.EnemyData.EnemyStatsData.Clear();
        _progressService.ProgressData.PurchasedCardCount.Clear();
        
        AddAllCardsToPool();
        
        _progressService.InitPurchasedCardCount(_cardIds);
        
        _progressService.InitStats();
        
        foreach (var enemy in _configProvider.GetEnemies())
        {
            _progressService.InitEnemyStats(enemy.EnemyId);
        }
        
        _hpProvider.InitHeroHp();
        
        _gameFactory.CreateHero(new Vector3(0,0,0));
        
        _progressService.SetMainCurrency(0);
    }

    private void AddAllCardsToPool()
    {
        foreach (var item in _configProvider.GetCardsConfig().AllNormalCardsByRareness)
        {
            foreach (var card in item.Value)
            {
                _cardIds.Add(card.CardId);
            }
        }
        foreach (var item in _configProvider.GetCardsConfig().AllUniqueCardsByRareness)
        {
            foreach (var card in item.Value)
            {
                _cardIds.Add(card.CardId);
            }
        }
    }
}
