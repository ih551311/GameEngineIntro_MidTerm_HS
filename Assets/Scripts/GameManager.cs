using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public UnityEngine.UI.Button gameStartButton;

    private void Start()
    {
        gameStartButton.onClick.AddListener(OnGameStartButtonClicked);
    }

    private void OnGameStartButtonClicked()
    {
        string PlayerName = inputField.text;
        if (string.IsNullOrEmpty(PlayerName))
        {
            Debug.Log("플레이어 이름을 입력하세요");
            return;
        }
        PlayerPrefs.SetString("PlayerName", inputField.text);
        PlayerPrefs.Save();

        Debug.Log("플레이어 이름 저장 됨: " + PlayerName);

        SceneManager.LoadScene("SpringScene");
    }

}