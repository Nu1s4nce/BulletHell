using UnityEngine;

public interface IInputService
{
    public Vector2 Direction { get; set; }
    public Vector2 GetKeyboardInput();
}