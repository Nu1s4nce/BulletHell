using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        Container.Bind<ITargetFinder>().To<TargetFinder>().AsSingle();
    }
}