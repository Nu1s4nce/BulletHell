using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public Camera mainCamera;

    public override void InstallBindings()
    {
        Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle().WithArguments(mainCamera);
        
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
        Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        Container.Bind<ITargetFinder>().To<TargetFinder>().AsSingle();
        Container.Bind<IHpProvider>().To<HPProvider>().AsSingle();
        Container.Bind<ICardsGenerator>().To<CardsGenerator>().AsSingle();
        
        Container.Bind<IInitializable>().To<LevelInitializer>().AsSingle();
    }
}
