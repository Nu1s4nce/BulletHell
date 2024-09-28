using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    private EnemyConfigData _enemyData;
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;

    private SpriteRenderer _sprite;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider)
    {
        _heroProvider = heroProvider;
        _configProvider = configProvider;
    }

    private void Awake()
    {
        _enemyData = _configProvider.GetEnemyConfig(0);
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        FlipSprite();
    }

    private void Move()
    {
        Vector3 distanceFromPlayer = (_heroProvider.GetHeroPosition() - transform.position).normalized;
        if (Vector3.Distance(_heroProvider.GetHeroPosition(), transform.position) > _enemyData.DistanceToAttack)
        {
            transform.position += (distanceFromPlayer * _enemyData.Speed * Time.deltaTime);
        }
        else
        {
            //Attack
        }
    }

    private void FlipSprite()
    {
        _sprite.flipX = _heroProvider.GetHeroPosition().x < transform.position.x;
    }
}