using UnityEngine;
using Zenject;

public class HeroAttack : MonoBehaviour
{
    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;
    
    private float attackRate = 1;
    private float timer;

    [SerializeField] private GameObject weaponPrefab;
    

    [Inject]
    private void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _configProvider = configProvider;
        _targetFinder = targetFinder;
    }

    private void Awake()
    {
        timer = attackRate;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = attackRate;
            _gameFactory.CreateProjectile(transform.position);
        }
    }
}