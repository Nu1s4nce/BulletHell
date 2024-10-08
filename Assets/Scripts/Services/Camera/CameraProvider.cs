using UnityEngine;

public class CameraProvider : ICameraProvider
{
    public Camera Camera { get; set; }

    public CameraProvider(Camera camera)
    {
        Camera = camera;
    }
}