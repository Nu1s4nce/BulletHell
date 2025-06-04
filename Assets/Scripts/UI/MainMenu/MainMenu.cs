using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
