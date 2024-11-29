using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyDamageHandler : MonoBehaviour, IDamageable, IIdHolder
{
    private int _enemyId;
    private float _currentHp;

    private Dictionary<CollectableType, float> tempDict = new();
    
    private EnemyAnimator _enemyAnimator;
    private readonly LootGenerator _lootGenerator = new();
    
    private IConfigProvider _configProvider;
    private ITargetFinder _targetFinder;
    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IConfigProvider configProvider, ITargetFinder targetFinder, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _targetFinder = targetFinder;
        _configProvider = configProvider;
    }
    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }

    private void Start()
    {
        _currentHp = _configProvider.GetEnemyConfig(_enemyId).MaxHp;
        tempDict.Add(CollectableType.MainCurrency, GetCollectableChances().CollectableChances[CollectableType.MainCurrency]);
        tempDict.Add(CollectableType.Food, GetCollectableChances().CollectableChances[CollectableType.Food]);
    }

    private void OnEnable()
    {
        _currentHp = _configProvider.GetEnemyConfig(_enemyId).MaxHp;
    }

    public void ApplyDamage(float damage)
    {
        _enemyAnimator.PlayDamageReceive();
        HandleTextPopup(damage);
        _currentHp -= damage;

        if (!(_currentHp <= 0)) return;
        
        Dead();
        SpawnCollectablesAfterDeath();
    }

    private void HandleTextPopup(float dmg)
    {
        Vector3 textPos = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        _gameFactory.CreateTextPopup(dmg, textPos);
    }

    private void Dead()
    {
        _targetFinder.RemoveTarget(transform);
        Destroy(gameObject);
    }

    private void SpawnCollectablesAfterDeath()
    {
        List<CollectableType> spawnList = _lootGenerator.GetLoot(tempDict);

        foreach (var collectable in spawnList)
        {
            if (collectable == CollectableType.MainCurrency)
            {
                int randCount = Random.Range(1, 6);
                for (int i = 0; i < randCount; i++)
                {
                    Vector3 pos = transform.position;
                    float randX = Random.Range(pos.x - 0.5f, pos.x + 0.5f);
                    float randY = Random.Range(pos.y - 0.5f, pos.y + 0.5f);
                    _gameFactory.CreateCollectable(new Vector3(randX, randY, 0), CollectableType.MainCurrency);
                }
            }
            else
            {
                _gameFactory.CreateCollectable(transform.position, collectable);
            }
        }
    }

    private LootChancesConfig GetCollectableChances()
    {
        return _configProvider.LootChancesConfig;
    }

    public void SetId(int id)
    {
        _enemyId = id;
    }
}