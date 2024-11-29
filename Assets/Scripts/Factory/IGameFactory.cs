using UnityEngine;

public interface IGameFactory
{
    GameObject CreateEnemy(int enemyId, Vector3 pos, Transform enemiesPoolParent);
    GameObject CreateHero(Vector3 pos);
    GameObject CreateTargetProjectile(GameObject prefab, Vector3 pos, Transform target, float damage, float speed);
    GameObject CreateCollisionProjectile(GameObject prefab, Vector3 pos, Transform target, float damage, float speed);
    GameObject CreateTextPopup(float dmg, Vector3 pos);
    GameObject CreateCollectable(Vector3 pos, CollectableType collectableType);
}