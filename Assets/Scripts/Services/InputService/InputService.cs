using UnityEngine;

public class InputService : IInputService
{
    public Vector2 Direction { get; set; }

    public Vector2 GetKeyboardInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public bool E_Clicked()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
    public bool Esc_Clicked()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}