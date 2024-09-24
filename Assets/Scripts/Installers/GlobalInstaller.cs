using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingle();
        Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        Container.Bind<ITargetFinder>().To<TargetFinder>().AsSingle();
        
        Container.Bind<IInitializable>().To<LevelInitializer>().AsSingle();
    }
}