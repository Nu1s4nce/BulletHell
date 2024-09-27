using UnityEngine;
using Zenject;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int shiftForce;
    
    private Vector2 direction;
    private bool b_shift;

    private Rigidbody2D rb;
    private SpriteRenderer _sprite;
    private HeroAnimator _heroAnimator;

    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _heroAnimator = GetComponent<HeroAnimator>();
        
    }
    private void Update()
    {
        MovementDirection();
        CheckInputs();
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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        FlipHero(direction);
        
        if(direction.x != 0 || direction.y != 0) _heroAnimator.PlayAnimation("Run");
        else _heroAnimator.PlayAnimation("Idle");
    }

    private void FlipHero(Vector2 dir)
    {
        if(dir.x == 0) return;
        _sprite.flipX = !(dir.x > 0);
    }
    
    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            b_shift = true;
        }
    }

    private void MovePlayer()
    {
        direction = Vector2.ClampMagnitude(direction, 1);
        rb.MovePosition(rb.position + GetHeroStats().MoveSpeed * Time.fixedDeltaTime * direction);
    }
}