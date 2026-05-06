using UnityEngine;

/// <summary>
/// 테스트용: 랜덤 기록을 자동으로 추가해주는 헬퍼
/// Inspector의 OnClick으로 직접 연결하는 방식
/// </summary>
public class LeaderboardTestHelper : MonoBehaviour
{
    [Header("설정")]
    public int recordsPerStage = 8;
    public float minTime = 35f;
    public float maxTime = 110f;

    private string[] sampleNames = {
        "SpeedKing", "RunnerX", "FlashGamer", "NinjaCat", "ProPlayer",
        "TimeMaster", "ShadowRun", "EpicGamer", "QuickSilver", "Legend47"
    };

    /// <summary>
    /// 랜덤 기록 추가 (Inspector OnClick에 연결하세요)
    /// </summary>
    public void AddRandomRecords()
    {
        for (int stage = 1; stage <= 5; stage++)
        {
            string stageId = $"Stage{stage}";

            for (int i = 0; i < recordsPerStage; i++)
            {
                string randomName = sampleNames[Random.Range(0, sampleNames.Length)] + Random.Range(1, 99);
                float randomTime = Random.Range(minTime, maxTime);

                LeaderboardManager.Instance.AddEntry(stageId, randomName, randomTime);
            }
        }

        Debug.Log($"[TestHelper] 5개 스테이지에 각각 {recordsPerStage}개의 랜덤 기록을 추가했습니다!");
    }

    /// <summary>
    /// 모든 기록 초기화 후 랜덤 기록 다시 추가
    /// </summary>
    public void ResetAndAddRandom()
    {
        LeaderboardManager.Instance.ClearAllData();
        AddRandomRecords();
    }
}