using System.Collections.Generic;

public class LootGenerator
{
    public List<CollectableType> GetLoot(Dictionary<CollectableType, float> spawnCollectables)
    {
        List<CollectableType> finalCollectableList = new();
        foreach (var collectable in spawnCollectables)
        {
            if (RandomService.GetItemByChanceOrNothing(collectable.Value))
            {
                finalCollectableList.Add(collectable.Key);
            }
        }
        return finalCollectableList;
    }
}