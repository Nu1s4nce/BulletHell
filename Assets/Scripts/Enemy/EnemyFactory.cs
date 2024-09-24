using System.Collections.Generic;
using UnityEngine;
public class EnemyFactory
{
    private List<EnemyConfigData> _enemyFactory;
    
    public void Init(List<EnemyConfigData> enemyConfigData)
    {
        _enemyFactory = enemyConfigData;
    }

    public EnemyConfigData CreateEnemy(int enemyId)
    {
        Debug.Log($"хп моба: {_enemyFactory[enemyId].MaxHp}, дмг {_enemyFactory[enemyId].Damage}");
        return _enemyFactory[enemyId];
    }
}