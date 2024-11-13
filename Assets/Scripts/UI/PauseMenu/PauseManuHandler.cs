using UnityEngine;

public class PauseManuHandler : MonoBehaviour
{

    public Animator settingsAnimator;
    public GameObject pausePanel;

    private bool _isPanelOpened;

    public void OnSettingsImageClick()
    {
        settingsAnimator.Play("Pressed", 0, 0.0f);
        _isPanelOpened = !_isPanelOpened;
        pausePanel.SetActive(_isPanelOpened);
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
        
    }
}
