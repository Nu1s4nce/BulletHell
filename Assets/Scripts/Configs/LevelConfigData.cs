using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigData", menuName = "LevelConfigData")]
public class LevelConfigData : ScriptableObject
{
    public List<EnemyConfigData> Enemies = new();
    public HeroConfigData HeroConfigData = new();
}
