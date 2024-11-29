using System;

public class ScoreService : IScoreService
{
    public float Score { get; set; }

    public event Action OnScoreChanged;
    
    public void AddScore(float value)
    {
        Score += value;
        OnScoreChanged?.Invoke();
    }

    public void RemoveScore(float value)
    {
        Score -= value;
        OnScoreChanged?.Invoke();
    }

    public float GetScore()
    {
        return Score;
    }
}