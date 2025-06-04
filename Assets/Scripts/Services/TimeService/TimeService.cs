public class TimeService : ITimeService
{
    private bool _paused;
    public bool IsPaused => _paused;
    public float DeltaTime => !_paused ? UnityEngine.Time.deltaTime : 0;
    public float FixedDeltaTime => !_paused ? UnityEngine.Time.fixedDeltaTime : 0;

    
    public void PauseGame() => _paused = true;
    public void ResumeGame() => _paused = false;
}

public interface ITimeService
{
    public bool IsPaused { get; }
    public float DeltaTime { get; }
    public float FixedDeltaTime { get; }
    public void PauseGame();
    public void ResumeGame();
}