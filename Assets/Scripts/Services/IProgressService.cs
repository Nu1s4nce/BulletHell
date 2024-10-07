public interface IProgressService
{
    public PlayerProgressData GetProgressData { get; set; }

    public int GetMainCurrency();
}