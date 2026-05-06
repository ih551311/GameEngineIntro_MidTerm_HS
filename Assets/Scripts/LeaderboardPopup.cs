using UnityEngine;
using TMPro;

/// <summary>
/// Inspector에서 OnClick으로 직접 연결하는 방식 (AddListener 사용 안 함)
/// </summary>
public class LeaderboardPopup : MonoBehaviour
{
    [Header("필수 UI 연결")]
    public GameObject entryPrefab;
    public Transform contentParent;


    private int currentStage = 1;

    private void Start()
    {
        // 처음에는 Stage 1 표시
        SwitchToStage1();
    }

    // ==================== Inspector에서 연결할 메서드들 ====================
    public void SwitchToStage1() { SwitchStage(1); }
    public void SwitchToStage2() { SwitchStage(2); }
    public void SwitchToStage3() { SwitchStage(3); }
    public void SwitchToStage4() { SwitchStage(4); }
    public void SwitchToStage5() { SwitchStage(5); }

    // ==================== 내부 공통 메서드 ====================
    private void SwitchStage(int stageNumber)
    {
        currentStage = stageNumber;
     

        // 기존 기록 삭제
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        string stageId = $"Stage{stageNumber}";
        var topEntries = LeaderboardManager.Instance.GetTopEntries(stageId, 5);

        for (int i = 0; i < topEntries.Count; i++)
        {
            var entry = topEntries[i];
            GameObject go = Instantiate(entryPrefab, contentParent);

            TextMeshProUGUI[] texts = go.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length >= 4)
            {
                texts[0].text = $"{i + 1}위";
                texts[1].text = entry.playerName;
                texts[2].text = FormatTime(entry.clearTime);
                texts[3].text = entry.dateTime;
            }
        }
    }

    private string FormatTime(float seconds)
    {
        int min = Mathf.FloorToInt(seconds / 60f);
        float sec = seconds % 60f;
        return $"{min:00}:{sec:00.00}";
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}