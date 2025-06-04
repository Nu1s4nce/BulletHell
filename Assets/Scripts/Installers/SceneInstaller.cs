using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public Camera mainCamera;
    [SerializeField] private SoundManager _soundManager;

    public override void InstallBindings()
    {
        Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle().WithArguments(mainCamera);
        Container.BindInterfacesAndSelfTo<SoundManager>()
            .FromInstance(_soundManager)
            .AsSingle();
        
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
        Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        Container.Bind<ITargetFinder>().To<TargetFinder>().AsSingle();
        Container.Bind<IHpProvider>().To<HPProvider>().AsSingle();
        Container.Bind<ICardsGenerator>().To<CardsGenerator>().AsSingle();
        Container.Bind<IGameStateService>().To<GameStateService>().AsSingle();
        
        
        Container.Bind<IInitializable>().To<LevelInitializer>().AsSingle();
    }
}
