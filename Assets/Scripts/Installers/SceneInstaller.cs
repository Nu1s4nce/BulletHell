using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public Camera mainCamera;

    public override void InstallBindings()
    {
        Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle().WithArguments(mainCamera);
    }
}
