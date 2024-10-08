using UnityEngine;

public interface IEnemyPoolProvider
{
    public void Init();
    public GameObject GetEnemy();
    public void ReturnEnemy(GameObject bullet);
}