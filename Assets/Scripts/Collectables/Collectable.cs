using DG.Tweening;
using UnityEngine;
using Zenject;

public class Collectable : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _startScale;
    private bool _isFlying;
    
    private IHeroProvider _heroProvider;
    private IConfigProvider _configProvider;
    private IProgressService _progressService;

    [Inject]
    public void Construct(IHeroProvider heroProvider, IConfigProvider configProvider, IProgressService progressService)
    {
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
        if (CanCollect())
        {
            Collect();
            _progressService.AddMainCurrency(1 + (int)GetProgressData().HeroStatsData[StatId.CollectablesValue]);
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
        transform.localScale = Vector3.Lerp(_startScale, new Vector3(0.15f,0.15f,0.15f), flyProgress);
    }
    private void UpdateScale(float scaleProgress)
    {
        transform.localScale = Vector3.Lerp(_startScale, new Vector3(0.5f,0.5f,0), scaleProgress);
    }

    private HeroProgressData GetProgressData()
    {
        return _progressService.GetHeroData();
    }
}