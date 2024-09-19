using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private Dictionary<string, Func<int, EnemyModel>> enemyFactory;

    public void Init(EnemyDescriptions descriptions)
    {
        enemyFactory = new Dictionary<string, Func<int, EnemyModel>>()
        {
            {"RangeMob", (level) => new EnemyModel(descriptions.ListRange[level])},
            {"MeleeMob", (level) => new EnemyModel(descriptions.ListMelee[level])}
        };

    }

    public EnemyModel CreateMobModel(string nameMob, int level)
    {
        Debug.Log($"Моб: {nameMob}, на сложности {level}");
        return enemyFactory[nameMob](level);
    }
}