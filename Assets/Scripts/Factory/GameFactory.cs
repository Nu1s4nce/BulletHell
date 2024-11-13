using TMPro;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IConfigProvider _configProvider;
    private readonly IHeroProvider _heroProvider;
    private readonly DiContainer _diContainer;
    private readonly ITargetFinder _targetFinder;

    public GameFactory(IConfigProvider configProvider, IHeroProvider heroProvider, DiContainer diContainer, ITargetFinder targetFinder)
    {
        _targetFinder = targetFinder;
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _diContainer = diContainer;
    }

    public GameObject CreateEnemy(int enemyId, Vector3 pos, Transform enemiesPoolParent)
    {
        EnemyConfigData config = _configProvider.GetEnemyConfig(enemyId);
        GameObject enemy =
            _diContainer.InstantiatePrefab(config.EnemyPrefab, pos, Quaternion.identity, enemiesPoolParent);
        foreach (var componentsInChild in enemy.GetComponentsInChildren<IIdHolder>())
        {
            componentsInChild.SetId(enemyId);
        }
        _targetFinder.AddTarget(enemy.transform);
        return enemy;
    }

    public GameObject CreateHero(Vector3 pos)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject hero = _diContainer.InstantiatePrefab(config.HeroPrefab, pos, Quaternion.identity, null);
        _heroProvider.Hero = hero;
        return hero;
    }
    public GameObject CreateTargetProjectile(GameObject prefab, Vector3 pos, Transform target, float damage, float speed)
    {
        GameObject inst = CreateProjectile(prefab, pos);
        
        if (inst.TryGetComponent(out TargetProjectileMover targetProjectileMovement))
        {
            targetProjectileMovement.Target = target;
            targetProjectileMovement.ProjectileDamage = damage;
            targetProjectileMovement.ProjectileSpeed = speed;
        }
        
        return inst;
    }
    public GameObject CreateCollisionProjectile(GameObject prefab, Vector3 pos, Transform target, float damage, float speed)
    {
        GameObject inst = CreateProjectile(prefab, pos);
        
        if (inst.TryGetComponent(out CollisionProjectileMover collisionProjectileMovement))
        {
            collisionProjectileMovement.Target = target;
            collisionProjectileMovement.ProjectileDamage = damage;
            collisionProjectileMovement.ProjectileSpeed = speed;
        }
        
        return inst;
    }
    private GameObject CreateProjectile(GameObject prefab, Vector3 pos)
    {
        GameObject projectile = _diContainer.InstantiatePrefab(prefab, pos, Quaternion.identity, null);
        return projectile;
    }
    public GameObject CreateAuraWeapon(Vector3 pos, Transform target, float speed, int damage)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject projectile = _diContainer.InstantiatePrefab(config.WeaponPrefab, pos, Quaternion.identity, null);
        
        return projectile;
    }

    public GameObject CreateTextPopup(float dmg, Vector3 pos)
    {
        GameObject textPrefab = _configProvider.GetTextPrefab();
        GameObject textPopup = _diContainer.InstantiatePrefab(textPrefab, pos, Quaternion.identity, null);
        textPopup.GetComponentInChildren<TMP_Text>().text = dmg.ToString();
        return textPopup;
    }

    public GameObject CreateCollectable(Vector3 pos)
    {
        GameObject collectablePrefab = _configProvider.GetCollectablePrefab();
        GameObject collectable = _diContainer.InstantiatePrefab(collectablePrefab, pos, Quaternion.identity, null);
        return collectable;
    }
}