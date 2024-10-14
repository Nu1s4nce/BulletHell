using UnityEngine;

public class TimerService
{
    private readonly float _timerTime;
    private float _currentTime;
    
    public TimerService(float timerTime)
    {
        _timerTime = _currentTime = timerTime;
    }

    public void UpdateTimer()
    {
        _currentTime -= Time.deltaTime;
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