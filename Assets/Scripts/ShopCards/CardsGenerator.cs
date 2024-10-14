using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsGenerator
{
    private int _numberOfCardsInShop = 3;

    private CardsConfig CardsConfig;
    
    public void GenerateCard()
    {
        var typeOfCard = GetRandomItemByChance(CardsConfig.TypeOfCard);
        if(typeOfCard == TypeOfCard.Unique) Debug.Log("UniqueCard reveal in shop");
        var rarenessOfCard = GetRandomItemByChance(CardsConfig.RarenessOfCards);
    }

    private T GetRandomItemByChance<T>(Dictionary<T, float> dict)
    {
        List<float> tempChancesList = new List<float>();
        List<T> tempItem = new List<T>();
        
        foreach (var chances in dict)
        {
            tempItem.Add(chances.Key);
            tempChancesList.Add(chances.Value);
        }

        var index = Choose(tempChancesList.ToArray());
        Debug.Log(tempItem[index]);
        return tempItem[index];
    }
    
    int Choose (float[] probs) {

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