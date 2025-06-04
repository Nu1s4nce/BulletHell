using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Collectable : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _startScale;
    private bool _isFlying;

    [SerializeField] private CollectableType _collectableType;
    
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;
    private IProgressService _progressService;
    private IHpProvider _hpProvider;
    private ISoundManager _soundManager;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider, IProgressService progressService, IHpProvider hpProvider, ISoundManager soundManager)
    {
        _soundManager = soundManager;
        _hpProvider = hpProvider;
        _progressService = progressService;
        _configProvider = configProvider;
        _heroProvider = heroProvider;
    }

    private void Awake()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;
    }

    public void Update()
    {
        if (!CanCollect()) return;

        Collect();
        
        switch (_collectableType)
        {
            case CollectableType.MainCurrency:
                _progressService.AddMainCurrency((int)_configProvider.GetHeroConfig().CollectablesValue + (int)GetProgressData().HeroStatsData[StatId.CollectablesValue]);
                _soundManager.PlayCurrencyPickUp();
                break;
            case CollectableType.Food:
                _hpProvider.AddHeroCurrentHp(_configProvider.GetHeroConfig().FoodHealValue + GetProgressData().HeroStatsData[StatId.FoodHealValue]);
                _soundManager.PlayFoodPickUp();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool CanCollect()
    {
        if (_isFlying) return false;
        
        var distanceToHero = Vector3.Distance(_startPos, _heroProvider.GetHeroPosition());
        return distanceToHero <= _configProvider.GetHeroConfig().CollectablesPickRange +
            GetProgressData().HeroStatsData[StatId.CollectablesPickRange];
    }
    private void Collect()
    {
        _isFlying = true;

        DOVirtual
            .Float(0f, 1f, 0.25f, UpdateScale)
            .OnComplete(() => DOVirtual
                .Float(0f, 1f, 0.5f, UpdateFly).SetEase(Ease.OutCubic)
                .OnComplete(() => Destroy(gameObject)));
    }

    private void UpdateFly(float flyProgress)
    {
        transform.position = Vector3.Lerp(_startPos, _heroProvider.GetHeroPosition(), flyProgress);
        transform.localScale = Vector3.Lerp(_startScale, new Vector3(2f,2f,0), flyProgress);
    }
    private void UpdateScale(float scaleProgress)
    {
        transform.localScale = Vector3.Lerp(_startScale, new Vector3(3f,3f,0), scaleProgress);
    }

    private HeroProgressData GetProgressData()
    {
        return _progressService.GetHeroData();
    }
}

public enum CollectableType
{
    MainCurrency = 0,
    Food = 1
}