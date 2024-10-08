using DG.Tweening;
using UnityEngine;
using Zenject;

public class Collectable : MonoBehaviour, ICollectable
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

    private void OnEnable()
    {
        
    }

    public void Update()
    {
        if (CanCollect())
        {
            Collect();
            _progressService.ProgressData.AddMainCurrency(1);
        }
    }
    
    public void OnCollect()
    {
        
    }

    private bool CanCollect()
    {
        if (_isFlying) return false;
        
        var distanceToHero = Vector3.Distance(_startPos, _heroProvider.GetHeroPosition());
        return distanceToHero <= _configProvider.GetHeroConfig().CollectablesPickRange;
    }
    private void Collect()
    {
        _isFlying = true;

        DOVirtual
            .Float(0f, 1f, 0.2f, UpdateScale)
            .OnComplete(() => DOVirtual
                .Float(0f, 1f, 0.4f, UpdateFly)
                .OnComplete(() => Destroy(gameObject)));
    }

    private void UpdateFly(float flyProgress)
    {
        transform.position = Vector3.Lerp(_startPos, _heroProvider.GetHeroPosition(), flyProgress);
    }
    private void UpdateScale(float scaleProgress)
    {
        transform.localScale = Vector3.Lerp(_startScale, new Vector3(0.6f,0.6f,0.6f), scaleProgress);
    }
}