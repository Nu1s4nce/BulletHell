using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create CardsConfig", fileName = "CardsConfig")]
public class CardsConfig : SerializedScriptableObject
{
    public Dictionary<RarenessOfCard, List<NormalCardConfig>> AllNormalCardsByRareness;
    public Dictionary<RarenessOfCard, List<UniqueCardConfig>> AllUniqueCardsByRareness;
    
}