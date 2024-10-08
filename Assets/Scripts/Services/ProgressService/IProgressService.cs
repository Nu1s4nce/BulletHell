public interface IProgressService
{
    public PlayerProgressData ProgressData { get; set; }

    public int GetMainCurrency();
}