using UnityEngine;
using Zenject;

public class PauseManuHandler : MonoBehaviour
{
    public Animator settingsAnimator;
    public GameObject pausePanel;
    public GameObject endGamePanel;

    private bool _isPanelOpened;
    private IHpProvider _hpProvider;
    private ITimeService _time;
    private IGameStateService _gameStateService;

    [Inject]
    public void Construct(IHpProvider hpProvider, ITimeService timeService, IGameStateService gameStateService)
    {
        _gameStateService = gameStateService;
        _time = timeService;
        _hpProvider = hpProvider;
    }

    private void Awake()
    {
        _hpProvider.PlayerDead += ShowEndGamePanel;
    }

    public void OnSettingsImageClick()
    {
        settingsAnimator.Play("Pressed", 0, 0.0f);
        _isPanelOpened = !_isPanelOpened;
        pausePanel.SetActive(_isPanelOpened);
        if (_isPanelOpened) _time.PauseGame();
        else _time.ResumeGame();
    }

    private void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);
        _time.PauseGame();
    }
    
    public void OnSettingsButtonClick()
    {
        
    }
    
    public void OnQuitGameButtonClick()
    {
        Debug.Log("Вышел");
        Application.Quit();
    }
    
    public void OnRestartGameButtonClick()
    {
        _gameStateService.RestartGame();
    }
}
