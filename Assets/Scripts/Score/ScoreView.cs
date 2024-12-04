using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    private Timer _scoreTimer;
    
    [SerializeField] private float _scoreRate;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float current;
    
    private IScoreService _scoreService;
    private ITimeService _time;

    [Inject]
    public void Construct(IScoreService scoreService, ITimeService timeService)
    {
        _time = timeService;
        _scoreService = scoreService;
    }

    private void Awake()
    {
        _scoreTimer = new Timer(_scoreRate, _time);
        _scoreService.OnScoreChanged += UpdateScoreView;
    }
    private void Update()
    {
        _scoreTimer.UpdateTimer();
        if (_scoreTimer.CheckTimerEnd())
        {
            _scoreService.AddScore(100);
            _scoreTimer.ResetTimer();
        }
    }
    
    private void UpdateScoreView()
    {
        DOVirtual.Float(current, _scoreService.GetScore(), 0.2f, (x) =>
        {
            _scoreText.text = "Score : " + (int)x;
        });
        
        current = _scoreService.GetScore();
    }
}