using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsGenerator : ICardsGenerator
{
    private IConfigProvider _configProvider;

    public CardsGenerator(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }

    public CardType GetTypeOfCardToGenerate()
    {
        return GetRandomItemByChance(GetCardsChancesConfig().TypeOfCard);
    }
    
    public NormalCardConfig GenerateNormalCard()
    {
        var cardRareness = GetRandomItemByChance(GetCardsChancesConfig().RarenessOfCards);
        return GetRandomItem(GetCardsConfig().AllNormalCardsByRareness[cardRareness]);
    }

    public UniqueCardConfig GenerateUniqueCard()
    {
        var cardRareness = GetRandomItemByChance(GetCardsChancesConfig().RarenessOfCards);
        return GetRandomItem(GetCardsConfig().AllUniqueCardsByRareness[cardRareness]);
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
        Debug.Log(tempItem[index]);
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