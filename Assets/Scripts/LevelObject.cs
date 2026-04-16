using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelObject : MonoBehaviour
{
    public string nextLevel;
    public void MoveToNextLevel()
    {
        Debug.Log("00");
        SceneManager.LoadScene(nextLevel);
    }
}