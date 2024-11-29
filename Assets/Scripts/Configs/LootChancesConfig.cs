using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create LootChancesConfig", fileName = "LootChancesConfig")]
public class LootChancesConfig : SerializedScriptableObject
{
    public Dictionary<CollectableType, float> CollectableChances;
    public Dictionary<CollectableType, GameObject> CollectablePrefabs;
}