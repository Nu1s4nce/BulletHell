using UnityEngine;
using Zenject;

public class HeroMover : MonoBehaviour
{
    [SerializeField] private int speed;
    
    private Vector2 _direction;
    private bool b_shift;

    private Rigidbody2D rb;
    private HeroAnimator _heroAnimator;

    private IConfigProvider _configProvider;
    private IInputService _inputService;

    [Inject]
    public void Construct(IConfigProvider configProvider, IInputService inputService)
    {
        _inputService = inputService;
        _configProvider = configProvider;
    }

    void Awake()
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
        rb.MovePosition(rb.position + GetHeroStats().MoveSpeed * Time.fixedDeltaTime * _direction);
    }
}