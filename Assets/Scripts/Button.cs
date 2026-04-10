using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("SpringScene");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
