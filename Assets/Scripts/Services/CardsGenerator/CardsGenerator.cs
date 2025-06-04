using System.Collections.Generic;

public class CardsGenerator : ICardsGenerator
{
    private readonly IConfigProvider _configProvider;
    private readonly IProgressService _progressService;

    public CardsGenerator(IConfigProvider configProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _configProvider = configProvider;
    }

    public CardType GetTypeOfCardToGenerate()
    {
        return RandomService.GetRandomItemByChance(GetCardsChancesConfig().TypeOfCard);
    }
    
    public CardsRarenessColors GetColorByRareness(RarenessOfCard rarenessOfCard)
    {
        return GetCardsConfig().AllCardColorsByRareness[rarenessOfCard];
    }
    public CardsRarenessColors GetUniqueCardColor()
    {
        return GetCardsConfig().UniqueCardColors;
    }
    
    public NormalCardConfig GenerateNormalCard()
    {
        var mainDict = GetCardsConfig().AllNormalCardsByRareness;
        Dictionary<RarenessOfCard, List<NormalCardConfig>> tempDict = new();
        
        Dictionary<RarenessOfCard, float> cardsRareness = GetCardsChancesConfig().RarenessOfCards;
        Dictionary<RarenessOfCard, float> cardsRarenessTemp = new();
        

        foreach (var card in mainDict)
        {
            List<NormalCardConfig> tempList = new();
            foreach (var cardConfig in card.Value)
            {
                if (cardConfig.PoolLimit - _progressService.ProgressData.PurchasedCardCount[cardConfig.CardId] > 0)
                {
                    tempList.Add(cardConfig);
                }
            }
            if (tempList.Count == 0) continue;
            tempDict.Add(card.Key, tempList);
            cardsRarenessTemp.Add(card.Key, cardsRareness[card.Key]);
        }

        RarenessOfCard cardRareness = RandomService.GetRandomItemByChance(cardsRarenessTemp);
        return RandomService.GetRandomItem(tempDict[cardRareness]);
    }

    public UniqueCardConfig GenerateUniqueCard(RarenessOfCard rarenessOfCard)
    {
        var mainDict = GetCardsConfig().AllUniqueCardsByRareness;
        Dictionary<RarenessOfCard, List<UniqueCardConfig>> tempDict = new();
        
        Dictionary<RarenessOfCard, float> cardsRareness = GetCardsChancesConfig().RarenessOfCards;
        Dictionary<RarenessOfCard, float> cardsRarenessTemp = new();
        

        foreach (var card in mainDict)
        {
            List<UniqueCardConfig> tempList = new();
            foreach (var cardConfig in card.Value)
            {
                if (cardConfig.PoolLimit - _progressService.ProgressData.PurchasedCardCount[cardConfig.CardId] > 0)
                {
                    tempList.Add(cardConfig);
                }
            }
            if (tempList.Count == 0) continue;
            tempDict.Add(card.Key, tempList);
            cardsRarenessTemp.Add(card.Key, cardsRareness[card.Key]);
        }

        RarenessOfCard cardRareness = RandomService.GetRandomItemByChance(cardsRarenessTemp);
        return RandomService.GetRandomItem(tempDict[cardRareness]);
    }
    
    private CardsChancesConfig GetCardsChancesConfig()
    {
        return _configProvider.GetCardsChancesConfig();
    }
    private CardsConfig GetCardsConfig()
    {
        return _configProvider.GetCardsConfig();
    }
    
    

    
}