public interface IConfigProvider
{
    LevelConfigData LevelConfig { get; }
    public void Load();
}