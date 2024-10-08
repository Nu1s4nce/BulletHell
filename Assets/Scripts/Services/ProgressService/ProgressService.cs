public class ProgressService : IProgressService
{
    public PlayerProgressData ProgressData { get; set; } = new();

    public int GetMainCurrency()
    {
        return ProgressData.MainCurrency;
    }
}