using UnityEngine;

public interface IGameFactory
{
    GameObject CreateEnemy(int enemyId, Vector3 pos, Transform enemiesPoolParent);
    GameObject CreateHero(Vector3 pos);
    GameObject CreateProjectile(Vector3 pos);
    GameObject CreateTextPopup(Vector3 pos);
    GameObject CreateCollectable(Vector3 pos);
}