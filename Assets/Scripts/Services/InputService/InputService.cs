using UnityEngine;

public class InputService : IInputService
{
    public Vector2 Direction { get; set; }

    public Vector2 GetKeyboardInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}