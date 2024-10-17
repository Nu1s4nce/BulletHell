using System.Collections.Generic;
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

    public RarenessOfCard GetRandomRarenessOfCard()
    {
        return GetRandomItemByChance(GetCardsChancesConfig().RarenessOfCards);
    }
    
    public CardsRarenessColors GetColorByRareness(RarenessOfCard rarenessOfCard)
    {
        return GetCardsConfig().AllCardColorsByRareness[rarenessOfCard];
    }
    
    public NormalCardConfig GenerateNormalCard(RarenessOfCard rarenessOfCard)
    {
        return GetRandomItem(GetCardsConfig().AllNormalCardsByRareness[rarenessOfCard]);
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