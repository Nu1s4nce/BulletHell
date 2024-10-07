public class ProgressService : IProgressService
{
    public PlayerProgressData GetProgressData { get; set; } = new();

    public int GetMainCurrency()
    {
        return GetProgressData.MainCurrency;
    }
}