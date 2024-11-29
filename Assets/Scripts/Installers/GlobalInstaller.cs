using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
        Container.Bind<ITimeService>().To<TimeService>().AsSingle();
    }
}