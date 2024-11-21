using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsGenerator : ICardsGenerator
{
    private IConfigProvider _configProvider;
    private IProgressService _progressService;

    public CardsGenerator(IConfigProvider configProvider, IProgressService progressService)
    {
        _progressService = progressService;
        _configProvider = configProvider;
    }

    public CardType GetTypeOfCardToGenerate()
    {
        return GetRandomItemByChance(GetCardsChancesConfig().TypeOfCard);
    }
    
    public CardsRarenessColors GetColorByRareness(RarenessOfCard rarenessOfCard)
    {
        return GetCardsConfig().AllCardColorsByRareness[rarenessOfCard];
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

        RarenessOfCard cardRareness = GetRandomItemByChance(cardsRarenessTemp);
        return GetRandomItem(tempDict[cardRareness]);
    }

    public UniqueCardConfig GenerateUniqueCard(RarenessOfCard rarenessOfCard)
    {
        return GetRandomItem(GetCardsConfig().AllUniqueCardsByRareness[rarenessOfCard]);
    }
    
    private CardsChancesConfig GetCardsChancesConfig()
    {
        return _configProvider.GetCardsChancesConfig();
    }
    private CardsConfig GetCardsConfig()
    {
        return _configProvider.GetCardsConfig();
    }
    
    private T GetRandomItem<T>(List<T>listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        return listToRandomize[randomNum];
    }
    private T GetRandomItemByChance<T>(Dictionary<T, float> dict)
    {
        List<float> tempChancesList = new List<float>();
        List<T> tempItem = new List<T>();
        
        foreach (var item in dict)
        {
            tempItem.Add(item.Key);
            tempChancesList.Add(item.Value);
        }

        int index = Choose(tempChancesList.ToArray());
        return tempItem[index];
    }
    
    private int Choose (float[] probs) {

        float total = 0;

        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i]) {
                return i;
            }
            randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }

    
}