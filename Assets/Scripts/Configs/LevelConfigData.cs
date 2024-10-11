using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigData", menuName = "LevelConfigData")]
public class LevelConfigData : ScriptableObject
{
    public HeroConfigData HeroConfigData = new();
    public List<EnemyConfigData> Enemies = new();
    public List<WeaponsConfigData> Weapons = new();

    public GameObject textPopUp;
    public GameObject collectable;
}
