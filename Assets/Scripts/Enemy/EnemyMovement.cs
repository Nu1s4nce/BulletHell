using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats _enemyStats;
    private IHeroProvider _heroProvider;

    [Inject]
    public void Construct(IHeroProvider heroProvider)
    {
        _heroProvider = heroProvider;
    }

    // private void FixedUpdate()
    // {
    //     Vector3 distanceFromPlayer = (_heroProvider.GetHeroPosition() - transform.position).normalized;
    //     if (Vector2.Distance(_heroProvider.GetHeroPosition(), transform.position) > 1.0f)
    //     {
    //         transform.position += (distanceFromPlayer * _enemyStats.speed * Time.deltaTime);
    //     }
    // }
}