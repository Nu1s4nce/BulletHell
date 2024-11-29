using System;

public interface IScoreService
{
    public event Action OnScoreChanged;
    public float Score { get; set; }

    public void AddScore(float value);
    public void RemoveScore(float value);
    public float GetScore();
}