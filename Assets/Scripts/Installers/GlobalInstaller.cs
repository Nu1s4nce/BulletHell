using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingle();
    }
}