public class TimeService : ITimeService
{
    private bool _paused;

    public float DeltaTime => !_paused ? UnityEngine.Time.deltaTime : 0;
    public float FixedDeltaTime => !_paused ? UnityEngine.Time.fixedDeltaTime : 0;

    public void StopTime() => _paused = true;
    public void StartTime() => _paused = false;
}

public interface ITimeService
{
    public float DeltaTime { get; }
    public float FixedDeltaTime { get; }
    public void StopTime();
    public void StartTime();
}