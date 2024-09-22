using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int shiftForce;


    private Vector2 direction;
    private bool b_shift;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void MovementDirection()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        transform.Translate(speed * Time.deltaTime * direction);
        //rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * direction);
    }
}