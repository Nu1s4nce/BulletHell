using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public Camera mainCamera;

    public override void InstallBindings()
    {
        Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle().WithArguments(mainCamera);
        
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<IEnemyPoolProvider>().To<EnemyPoolProvider>().AsSingle();
        Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        Container.Bind<ITargetFinder>().To<TargetFinder>().AsSingle();
        Container.Bind<IHpProvider>().To<HPProvider>().AsSingle();
        
        Container.Bind<IInitializable>().To<LevelInitializer>().AsSingle();
    }
}
