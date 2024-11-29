public class Timer
{
    private float _timerTime;
    private float _currentTime;
    private readonly ITimeService _time;

    public Timer(float timerTime, ITimeService timeService)
    {
        _timerTime = _currentTime = timerTime;
        _time = timeService;
    }

    public void UpdateTimer()
    {
        _currentTime -= _time.DeltaTime;
    }
    public void ChangeTimerMaxTime(float time)
    {
        _timerTime = time;
    }

    public float GetCurrentTime()
    {
        return _currentTime;
    }

    public bool CheckTimerEnd()
    {
        return _currentTime <= 0;
    }

    public void ResetTimer()
    {
        _currentTime = _timerTime;
    }
    
}