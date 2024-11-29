using UnityEngine;
using Zenject;

public class HeroMover : MonoBehaviour
{
    private Vector2 _direction;

    private Rigidbody2D rb;
    private HeroAnimator _heroAnimator;
    
    private IInputService _inputService;
    private IProgressService _progressService;
    private IConfigProvider _configProvider;
    private ITimeService _time;

    [Inject]
    public void Construct(IProgressService progressService, IInputService inputService, IConfigProvider configProvider, ITimeService timeService)
    {
        _time = timeService;
        _configProvider = configProvider;
        _progressService = progressService;
        _inputService = inputService;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _heroAnimator = GetComponent<HeroAnimator>();
        
    }
    private void Update()
    {
        MovementDirection();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private HeroConfigData GetHeroStats()
    {
        return _configProvider.GetHeroConfig();
    }
    private HeroProgressData GetHeroStatsBonus()
    {
        return _progressService.GetHeroData();
    }

    private void MovementDirection()
    {
        _direction = _inputService.GetKeyboardInput();
        _heroAnimator.LookAt(_direction);
        
        if(_direction.x != 0 || _direction.y != 0) _heroAnimator.PlayRun();
        else _heroAnimator.PlayIdle();
    }

    private void MovePlayer()
    {
        _direction = Vector2.ClampMagnitude(_direction, 1);
        rb.MovePosition(rb.position + (GetHeroStats().MoveSpeed + GetHeroStatsBonus().HeroStatsData[StatId.MoveSpeed]) * _time.FixedDeltaTime * _direction);
    }
    
}