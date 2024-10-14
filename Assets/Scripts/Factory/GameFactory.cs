using TMPro;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private readonly IConfigProvider _configProvider;
    private readonly IHeroProvider _heroProvider;
    private readonly DiContainer _diContainer;

    public GameFactory(IConfigProvider configProvider, IHeroProvider heroProvider, DiContainer diContainer)
    {
        _configProvider = configProvider;
        _heroProvider = heroProvider;
        _diContainer = diContainer;
    }

    public GameObject CreateEnemy(int enemyId, Vector3 pos, Transform enemiesPoolParent)
    {
        EnemyConfigData config = _configProvider.GetEnemyConfig(enemyId);
        GameObject enemy =
            _diContainer.InstantiatePrefab(config.EnemyPrefab, pos, Quaternion.identity, enemiesPoolParent);
        return enemy;
    }

    public GameObject CreateHero(Vector3 pos)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject hero = _diContainer.InstantiatePrefab(config.HeroPrefab, pos, Quaternion.identity, null);
        _heroProvider.Hero = hero;
        return hero;
    }
    

    public GameObject CreateProjectile(Vector3 pos, Transform target, int damage, float speed)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject projectile = _diContainer.InstantiatePrefab(config.WeaponPrefab, pos, Quaternion.identity, null);
        if (projectile.TryGetComponent(out TargetProjectileMover targetProjectileMovement))
        {
            targetProjectileMovement.Target = target;
            targetProjectileMovement.ProjectileDamage = damage;
            targetProjectileMovement.ProjectileSpeed = speed;
        }
        
        return projectile;
    }
    public GameObject CreateAuraWeapon(Vector3 pos, Transform target, float speed, int damage)
    {
        HeroConfigData config = _configProvider.GetHeroConfig();
        GameObject projectile = _diContainer.InstantiatePrefab(config.WeaponPrefab, pos, Quaternion.identity, null);
        
        return projectile;
    }

    public GameObject CreateTextPopup(int dmg, Vector3 pos)
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