using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigData", menuName = "LevelConfigData")]
public class LevelConfigData : ScriptableObject
{
    [Header("Hero Data")]
    public HeroConfigData HeroConfigData = new();
    [Header("Enemies Data")]
    public List<EnemyConfigData> Enemies = new();
    [Header("Weapons Data")]
    public List<WeaponsConfigData> Weapons = new();

    [Header("Helper Prefabs")]
    public GameObject textPopUp;
    
    [Header("Helper variables")]
    public int rerollCost;
}
