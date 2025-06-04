using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create CardsConfig", fileName = "CardsConfig")]
public class CardsConfig : SerializedScriptableObject
{
    public Dictionary<RarenessOfCard, List<NormalCardConfig>> AllNormalCardsByRareness;
    public Dictionary<RarenessOfCard, List<UniqueCardConfig>> AllUniqueCardsByRareness;
    public Dictionary<RarenessOfCard, CardsRarenessColors> AllCardColorsByRareness;
    public CardsRarenessColors UniqueCardColors;
}

public class CardsRarenessColors
{
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color BorderColor;
    
    [Header("Material")]
    public Material RarenessMaterial;
    public Color MaterialColor;
}
