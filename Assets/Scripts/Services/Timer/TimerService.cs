using UnityEngine;

public class TimerService
{
    private readonly float _timerTime;
    private float _currentTime;
    
    public TimerService(float timerTime)
    {
        _timerTime = timerTime;
        _currentTime = _timerTime;
    }

    public void UpdateTimer()
    {
        _currentTime -= Time.deltaTime;
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