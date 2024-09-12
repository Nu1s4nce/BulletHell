using UnityEngine;

public class InputService
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    public float Horizontal => Input.GetAxis(HorizontalAxis);
    public float Vertical => Input.GetAxis(VerticalAxis);

    //public bool IsRespawnButton() => UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.R);
}