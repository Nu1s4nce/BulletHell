using UnityEngine.SceneManagement;

public class GameStateService : IGameStateService
{
    private IProgressService _progressService;

    public GameStateService(IProgressService progressService)
    {
        _progressService = progressService;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}