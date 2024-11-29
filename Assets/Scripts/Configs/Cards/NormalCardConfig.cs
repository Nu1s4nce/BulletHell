using System.Collections.Generic;
using UnityEngine;

public class NormalCardConfig : Card
{
    [Header("Buffs")] public Dictionary<StatId, float> Stats = new();
}