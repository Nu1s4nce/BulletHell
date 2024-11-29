using System.Collections.Generic;
using UnityEngine;

public class RandomService
{
    public static bool GetItemByChanceOrNothing(float chance)
    {
        float randomNum = Random.Range(0, 100);
        return randomNum <= chance;
    }
    
    public static T GetRandomItem<T>(List<T>listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        return listToRandomize[randomNum];
    }
    public static T GetRandomItemByChance<T>(Dictionary<T, float> dict)
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
    
    private static int Choose (float[] probs) {

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