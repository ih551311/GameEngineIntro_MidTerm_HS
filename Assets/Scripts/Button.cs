using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject helpPanel;
    private void Start() // 뒤에
    {
        helpPanel.SetActive(false);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("SpringScene");
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }
    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
